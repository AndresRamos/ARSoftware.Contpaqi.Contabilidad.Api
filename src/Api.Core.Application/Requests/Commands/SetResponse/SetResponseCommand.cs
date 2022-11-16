using Api.SharedKernel.Models;
using MediatR;

namespace Api.Core.Application.Requests.Commands.SetResponse;

public sealed class SetResponseCommand : IRequest
{
    public SetResponseCommand(Guid id, Response response)
    {
        Id = id;
        Response = response;
    }

    public Guid Id { get; }
    public Response Response { get; }
}
