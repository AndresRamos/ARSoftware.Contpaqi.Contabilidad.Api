using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Api.Commands.ProcessApiRequest;
using Api.Sync.Core.Application.Api.Queries.GetPendingRequests;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Commands.AbrirEmpresa;
using MediatR;
using Microsoft.Extensions.Options;

namespace Api.Sync.Presentation.WorkerService;

public sealed class Worker : BackgroundService
{
    private readonly ApiSyncConfig _apiSyncConfig;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<Worker> _logger;
    private readonly IMediator _mediator;

    public Worker(ILogger<Worker> logger,
                  IMediator mediator,
                  IOptions<ApiSyncConfig> apiSyncConfigOptions,
                  IHostApplicationLifetime hostApplicationLifetime)
    {
        _logger = logger;
        _mediator = mediator;
        _hostApplicationLifetime = hostApplicationLifetime;
        _apiSyncConfig = apiSyncConfigOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _mediator.Send(new AbrirEmpresaCommand(), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                Task waitingTask = Task.Delay(_apiSyncConfig.WaitTime.ToTimeSpan(), stoppingToken);

                List<ApiRequestBase> apiRequests = (await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken)).ToList();

                foreach (ApiRequestBase apiRequest in apiRequests)
                {
                    int requestIndex = apiRequests.IndexOf(apiRequest) + 1;
                    int requestsCount = apiRequests.Count;
                    _logger.LogInformation("Processing request [{requestIndex} of {requestsCount}]", requestIndex, requestsCount);

                    await _mediator.Send(new ProcessApiRequestCommand(apiRequest), stoppingToken);
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
        catch (Exception e)
        {
            _logger.LogCritical(e, "Critical error ocurred.");
        }

        _hostApplicationLifetime.StopApplication();
    }
}
