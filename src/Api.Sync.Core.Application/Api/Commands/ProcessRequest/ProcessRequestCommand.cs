using Api.SharedKernel.Models;
using MediatR;

namespace Api.Sync.Core.Application.Api.Commands.ProcessRequest;

public class ProcessRequestCommand : IRequest
{
    public ProcessRequestCommand(Request request)
    {
        Request = request;
    }

    public Request Request { get; }
}
