using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
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
            while (!stoppingToken.IsCancellationRequested)
            {
                Task waitingTask = Task.Delay(_apiSyncConfig.WaitTime.ToTimeSpan(), stoppingToken);

                List<ApiRequestBase> pendingRequests = (await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken)).ToList();
                _logger.LogInformation("{PendingRequests} solicitudes pendientes.", pendingRequests.Count);

                foreach (IGrouping<string, ApiRequestBase> requestGroup in pendingRequests.GroupBy(request => request.EmpresaRfc))
                {
                    Empresa? empresa = await _empresaRepository.BuscarPorRfcAsync(requestGroup.Key, LoadRelatedDataOptions.Default,
                        stoppingToken);

                    if (empresa is null)
                        continue;

                    _contpaqiContabilidadConfig.Empresa = empresa;

                    await _mediator.Send(new AbrirEmpresaCommand(), stoppingToken);

                    List<ApiRequestBase> apiRequests = requestGroup.ToList();
                    foreach (ApiRequestBase apiRequest in apiRequests)
                    {
                        int requestIndex = apiRequests.IndexOf(apiRequest) + 1;
                        int requestsCount = apiRequests.Count;
                        _logger.LogInformation("Empresa: {EmpresaRfc}. Procesando [{requestIndex} of {requestsCount}]", requestGroup.Key,
                            requestIndex, requestsCount);

                        await _mediator.Send(new ProcessApiRequestCommand(apiRequest), stoppingToken);
                    }
                }

                if (_apiSyncConfig.ShouldShutDown())
                {
                    _logger.LogInformation("La aplicacion debe apgarse.");
                    break;
                }

                if (_apiSyncConfig.WaitTime != TimeOnly.MinValue)
                {
                    _logger.LogDebug("Esperando la siguiente iteracion.");
                    await waitingTask;
                }
            }
        }
        catch (OperationCanceledException e)
        {
            _logger.LogWarning(e, "La operacion fue cancelada.");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Ocurrio un error critico.");
        }

        _hostApplicationLifetime.StopApplication();
    }
}
