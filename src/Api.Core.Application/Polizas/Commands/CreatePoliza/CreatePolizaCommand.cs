using Api.SharedKernel.Requests;
using MediatR;

namespace Api.Core.Application.Polizas.Commands.CreatePoliza;

public sealed class CreatePolizaCommand : IRequest<Guid>
{
    public CreatePolizaCommand(CreatePolizaRequest apiRequest)
    {
        ApiRequest = apiRequest;
    }

    public CreatePolizaRequest ApiRequest { get; }
}
