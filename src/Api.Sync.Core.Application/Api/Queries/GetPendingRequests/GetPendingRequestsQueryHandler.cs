using Api.SharedKernel.Models;
using Api.Sync.Core.Application.Api.Interfaces;

namespace Api.Sync.Core.Application.Api.Queries.GetPendingRequests;

public sealed class GetPendingRequestsQueryHandler : MediatR.IRequestHandler<GetPendingRequestsQuery, IEnumerable<Request>>
{
    private readonly IRequestRepository _requestRepository;

    public GetPendingRequestsQueryHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<IEnumerable<Request>> Handle(GetPendingRequestsQuery request, CancellationToken cancellationToken)
    {
        return (await _requestRepository.GetPendingRequestsAsync(cancellationToken)).ToList();
    }
}
