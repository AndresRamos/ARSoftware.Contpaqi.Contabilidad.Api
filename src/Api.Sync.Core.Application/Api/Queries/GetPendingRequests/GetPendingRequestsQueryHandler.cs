using Api.SharedKernel.Common;
using Api.Sync.Core.Application.Api.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Api.Queries.GetPendingRequests;

public sealed class GetPendingRequestsQueryHandler : IRequestHandler<GetPendingRequestsQuery, IEnumerable<ApiRequestBase>>
{
    private readonly IApiRequestRepository _apiRequestRepository;

    public GetPendingRequestsQueryHandler(IApiRequestRepository apiRequestRepository)
    {
        _apiRequestRepository = apiRequestRepository;
    }

    public async Task<IEnumerable<ApiRequestBase>> Handle(GetPendingRequestsQuery request, CancellationToken cancellationToken)
    {
        return (await _apiRequestRepository.GetPendingRequestsAsync(cancellationToken)).ToList();
    }
}
