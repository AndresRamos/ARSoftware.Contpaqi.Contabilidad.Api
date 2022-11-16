using Api.SharedKernel.Models;
using Api.Sync.Core.Application.Api.Interfaces;
using Api.Sync.Core.Application.Cuentas.Commands.CreateCuenta;
using Api.Sync.Core.Application.Polizas.Commands.CreatePoliza;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Api.Commands.ProcessRequest;

public class ProcessRequestCommandHandler : IRequestHandler<ProcessRequestCommand>
{
    private readonly ILogger<ProcessRequestCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IRequestRepository _requestRepository;

    public ProcessRequestCommandHandler(IMediator mediator, ILogger<ProcessRequestCommandHandler> logger,
                                        IRequestRepository requestRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _requestRepository = requestRepository;
    }

    public async Task<Unit> Handle(ProcessRequestCommand request, CancellationToken cancellationToken)
    {
        Request apiRequest = request.Request;
        Response? apiResponse = null;

        if (apiRequest is CreatePolizaRequest crearPolizaRequest)
            apiResponse = await _mediator.Send(new CreatePolizaCommand(crearPolizaRequest), cancellationToken);

        if (apiRequest is CreateCuentaRequest createCuentaRequest)
            apiResponse = await _mediator.Send(new CreateCuentaCommand(createCuentaRequest), cancellationToken);

        if (apiResponse != null)
            await _requestRepository.SetResponseAsync(apiRequest.Id, apiResponse, cancellationToken);

        return Unit.Value;
    }
}
