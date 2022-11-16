using Api.SharedKernel.Models;
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
                    IEnumerable<Request> requests = await _mediator.Send(new GetPendingRequestsQuery(), stoppingToken);

                    foreach (Request request in requests)
                        await _mediator.Send(new ProcessRequestCommand(request), stoppingToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error ocurred.");
                }
                finally
                {
                    await Task.Delay(50000, stoppingToken);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error ocurred.");
        }
        finally
        {
            await _mediator.Send(new CloseCompanyCommand(), stoppingToken);
            await _mediator.Send(new TerminateSdkCommand(), stoppingToken);
        }
    }
}
