using Api.Core.Application.Common;
using Api.SharedKernel.Models;
using MediatR;

namespace Api.Core.Application.Cuentas.Commands;

public sealed class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCuentaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCuentaCommand request, CancellationToken cancellationToken)
    {
        var createCuentaRequest = CreateCuentaRequest.CreateNew(request.Model);

        _context.Requests.Add(createCuentaRequest);

        await _context.SaveChangesAsync(cancellationToken);

        return createCuentaRequest.Id;
    }
}
