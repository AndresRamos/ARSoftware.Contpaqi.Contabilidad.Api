using Api.SharedKernel.Common;
using MediatR;

namespace Api.Sync.Core.Application.Api.Commands.ProcessRequest;

public class ProcessRequestCommand : IRequest
{
    public ProcessRequestCommand(ApiRequestBase request)
    {
        Request = request;
    }

    public ApiRequestBase Request { get; }
}
