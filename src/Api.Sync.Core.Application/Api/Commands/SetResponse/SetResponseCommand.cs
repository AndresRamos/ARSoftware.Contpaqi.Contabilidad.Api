using Api.SharedKernel.Common;
using MediatR;

namespace Api.Sync.Core.Application.Api.Commands.SetResponse;

public sealed class SetResponseCommand : IRequest
{
    public SetResponseCommand(Guid requestId, ApiResponseBase response)
    {
        RequestId = requestId;
        Response = response;
    }

    public Guid RequestId { get; }
    public ApiResponseBase Response { get; }
}
