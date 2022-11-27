using Api.Core.Application.Common;
using Api.SharedKernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetPendingRequests;

public sealed class GetPendingRequestsQueryHandler : IRequestHandler<GetPendingRequestsQuery, IEnumerable<ApiRequestBase>>
{
    private readonly IApplicationDbContext _context;

    public GetPendingRequestsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApiRequestBase>> Handle(GetPendingRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Requests.Where(r => r.IsProcessed == false).ToListAsync(cancellationToken);
    }
}
