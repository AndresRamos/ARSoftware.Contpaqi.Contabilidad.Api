using Api.SharedKernel.Models;
using MediatR;

namespace Api.Sync.Core.Application.Polizas.Commands.CreatePoliza;

public sealed class CreatePolizaCommand : IRequest<CreatePolizaResponse>
{
    public CreatePolizaCommand(CreatePolizaRequest request)
    {
        Request = request;
    }

    public CreatePolizaRequest Request { get; }
}
