using Api.SharedKernel.Requests;
using MediatR;

namespace Api.Core.Application.Cuentas.Commands;

public sealed class CreateCuentaCommand : IRequest<Guid>
{
    public CreateCuentaCommand(CreateCuentaRequest apiRequest)
    {
        ApiRequest = apiRequest;
    }

    public CreateCuentaRequest ApiRequest { get; }
}
