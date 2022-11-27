using Api.SharedKernel.Requests;
using MediatR;

namespace Api.Sync.Core.Application.Polizas.Commands.CreatePoliza;

public sealed class CreatePolizaCommand : IRequest<CreatePolizaResponse>
{
    public CreatePolizaCommand(CreatePolizaRequest apiRequest)
    {
        ApiRequest = apiRequest;
    }

    public CreatePolizaRequest ApiRequest { get; }
}
