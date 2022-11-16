using Api.Core.Application.Common;
using Api.SharedKernel.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetPendingRequests;

public class GetPendingRequestsQueryHandler : IRequestHandler<GetPendingRequestsQuery, IEnumerable<Request>>
{
    private readonly IApplicationDbContext _context;

    public GetPendingRequestsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Request>> Handle(GetPendingRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Requests.Where(r => r.IsProcessed == false).ToListAsync(cancellationToken);
    }
}
