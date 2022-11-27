using Api.SharedKernel.Common;
using Api.Sync.Core.Application.Api.Commands.ProcessRequest;
using Api.Sync.Core.Application.Api.Queries.GetPendingRequests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Commands.CloseCompany;
using Api.Sync.Core.Application.ContpaqiContabilidad.Commands.InitializeSdk;
using Api.Sync.Core.Application.ContpaqiContabilidad.Commands.OpenCompany;
using Api.Sync.Core.Application.ContpaqiContabilidad.Commands.TerminateSdk;
using MediatR;

namespace Api.Sync.Presentation.WorkerService;

public sealed class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMediator _mediator;

    public Worker(ILogger<Worker> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _mediator.Send(new InitializeSdkCommand(), stoppingToken);
            await _mediator.Send(new OpenCompanyCommand(), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    List<ApiRequestBase> requests = (await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken)).ToList();

                    foreach (ApiRequestBase request in requests)
                    {
                        int requestIndex = requests.IndexOf(request) + 1;
                        int requestsCount = requests.Count;
                        _logger.LogInformation("Processing request [{requestIndex} of {requestsCount}]", requestIndex, requestsCount);
                        await _mediator.Send(new ProcessRequestCommand(request), stoppingToken);
                    }

                    TimeSpan timeSpan = TimeSpan.FromMinutes(10);
                    _logger.LogInformation("Waiting {TimeSpan} for next run.", timeSpan);
                    await Task.Delay(timeSpan, stoppingToken);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e, "Critical error ocurred.");
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Critical error ocurred.");
        }
        finally
        {
            await _mediator.Send(new CloseCompanyCommand(), stoppingToken);
            await _mediator.Send(new TerminateSdkCommand(), stoppingToken);
        }
    }
}
