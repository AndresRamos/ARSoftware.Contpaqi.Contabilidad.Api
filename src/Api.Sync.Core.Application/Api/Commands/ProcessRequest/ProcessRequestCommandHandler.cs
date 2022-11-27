using Api.SharedKernel.Common;
using Api.SharedKernel.Requests;
using Api.Sync.Core.Application.Api.Commands.SetResponse;
using Api.Sync.Core.Application.Cuentas.Commands.CreateCuenta;
using Api.Sync.Core.Application.Polizas.Commands.CreatePoliza;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Api.Commands.ProcessRequest;

public class ProcessRequestCommandHandler : IRequestHandler<ProcessRequestCommand>
{
    private readonly ILogger<ProcessRequestCommandHandler> _logger;
    private readonly IMediator _mediator;

    public ProcessRequestCommandHandler(IMediator mediator, ILogger<ProcessRequestCommandHandler> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Unit> Handle(ProcessRequestCommand request, CancellationToken cancellationToken)
    {
        ApiRequestBase apiRequest = request.Request;
        ApiResponseBase? apiResponse = null;

        try
        {
            switch (apiRequest)
            {
                case CreatePolizaRequest crearPolizaRequest:
                    apiResponse = await _mediator.Send(new CreatePolizaCommand(crearPolizaRequest), cancellationToken);
                    break;
                case CreateCuentaRequest createCuentaRequest:
                    apiResponse = await _mediator.Send(new CreateCuentaCommand(createCuentaRequest), cancellationToken);
                    break;
            }
        }
        catch (ValidationException e)
        {
        }

        if (apiResponse != null)
            await _mediator.Send(new SetResponseCommand(apiRequest.Id, apiResponse), cancellationToken);

        return Unit.Value;
    }
}
