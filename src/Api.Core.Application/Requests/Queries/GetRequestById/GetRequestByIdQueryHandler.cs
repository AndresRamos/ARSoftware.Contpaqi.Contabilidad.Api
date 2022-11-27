using Api.Core.Application.Common;
using Api.SharedKernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Queries.GetRequestById;

public class GetRequestByIdQueryHandler : IRequestHandler<GetRequestByIdQuery, ApiRequestBase?>
{
    private readonly IApplicationDbContext _context;

    public GetRequestByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiRequestBase?> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Requests.Include(r => r.Response).Where(r => r.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
    }
}
