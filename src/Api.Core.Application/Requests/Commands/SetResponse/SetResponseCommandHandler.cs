using Api.Core.Application.Common;
using Api.SharedKernel.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Requests.Commands.SetResponse;

public sealed class SetResponseCommandHandler : IRequestHandler<SetResponseCommand>
{
    private readonly IApplicationDbContext _context;

    public SetResponseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(SetResponseCommand request, CancellationToken cancellationToken)
    {
        ApiRequestBase? apiRequest = await _context.Requests.Where(r => r.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        apiRequest.IsProcessed = true;
        apiRequest.Response = request.Response;
        apiRequest.Response.DateCreated = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
