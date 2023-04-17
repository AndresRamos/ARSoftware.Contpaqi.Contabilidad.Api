using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Api.Commands.ProcessApiRequest;
using Api.Sync.Core.Application.Api.Queries.GetPendingRequests;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Commands.AbrirEmpresa;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;

namespace Api.Sync.Presentation.WorkerService;

public sealed class Worker : BackgroundService
{
    private readonly ApiSyncConfig _apiSyncConfig;
    private readonly ContpaqiContabilidadConfig _contpaqiContabilidadConfig;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly IMediator _mediator;

    public Worker(ILogger<Worker> logger, IMediator mediator, IOptions<ApiSyncConfig> apiSyncConfigOptions,
        IHostApplicationLifetime hostApplicationLifetime, IOptions<ContpaqiContabilidadConfig> contpaqiContabilidadConfigOptions,
        IEmpresaRepository empresaRepository)
    {
        _logger = logger;
        _mediator = mediator;
        _hostApplicationLifetime = hostApplicationLifetime;
        _empresaRepository = empresaRepository;
        _contpaqiContabilidadConfig = contpaqiContabilidadConfigOptions.Value;
        _apiSyncConfig = apiSyncConfigOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            Task waitingTask = Task.Delay(_apiSyncConfig.WaitTime.ToTimeSpan(), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                List<IGrouping<string, ApiRequestBase>> requestGroups = (await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken))
                    .GroupBy(request => request.EmpresaRfc)
                    .ToList();

                foreach (IGrouping<string, ApiRequestBase> requestGroup in requestGroups)
                {
                    _contpaqiContabilidadConfig.Empresa =
                        await _empresaRepository.BuscarPorRfcAsync(requestGroup.Key, LoadRelatedDataOptions.Default, stoppingToken) ??
                        throw new InvalidOperationException();

                    await _mediator.Send(new AbrirEmpresaCommand(), stoppingToken);

                    foreach (ApiRequestBase apiRequest in requestGroup.ToList())
                    {
                        int requestIndex =
                            (await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken)).ToList().IndexOf(apiRequest) + 1;
                        int requestsCount = (await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken)).ToList().Count;
                        _logger.LogInformation("Processing request [{requestIndex} of {requestsCount}]", requestIndex, requestsCount);

                        await _mediator.Send(new ProcessApiRequestCommand(apiRequest), stoppingToken);
                    }
                }

                if (_apiSyncConfig.ShouldShutDown())
                {
                    _logger.LogInformation("Application should shut down.");
                    break;
                }

                if (_apiSyncConfig.WaitTime != TimeOnly.MinValue)
                {
                    _logger.LogDebug("Waiting for next run.");
                    await waitingTask;
                }
            }
        }
        catch (OperationCanceledException e)
        {
            _logger.LogWarning(e, "Operation was cancelled.");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Critical error ocurred.");
        }

        _hostApplicationLifetime.StopApplication();
    }
}
