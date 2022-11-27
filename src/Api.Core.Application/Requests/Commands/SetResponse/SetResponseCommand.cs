using Api.SharedKernel.Common;
using Api.SharedKernel.Models;
using MediatR;

namespace Api.Core.Application.Requests.Commands.SetResponse;

public sealed class SetResponseCommand : IRequest
{
    public SetResponseCommand(Guid id, ApiResponseBase response)
    {
        Id = id;
        Response = response;
    }

    public Guid Id { get; }
    public ApiResponseBase Response { get; }
}
