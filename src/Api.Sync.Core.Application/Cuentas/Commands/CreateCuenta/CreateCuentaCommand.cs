using Api.SharedKernel.Requests;
using MediatR;

namespace Api.Sync.Core.Application.Cuentas.Commands.CreateCuenta;

public sealed class CreateCuentaCommand : IRequest<CreateCuentaResponse>
{
    public CreateCuentaCommand(CreateCuentaRequest apiRequest)
    {
        ApiRequest = apiRequest;
    }

    public CreateCuentaRequest ApiRequest { get; }
}
