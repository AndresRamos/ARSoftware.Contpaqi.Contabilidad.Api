using Api.Core.Application.Common;
using Api.SharedKernel.Models;
using MediatR;

namespace Api.Core.Application.Polizas.Commands.CreatePoliza;

public sealed class CreatePolizaCommandHandler : IRequestHandler<CreatePolizaCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreatePolizaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreatePolizaCommand request, CancellationToken cancellationToken)
    {
        var crearPolizaRequest = new CreatePolizaRequest();
        crearPolizaRequest.DateCreated = DateTime.Now;
        crearPolizaRequest.Options = request.Options;
        crearPolizaRequest.Model = request.Model;
        _context.Requests.Add(crearPolizaRequest);

        await _context.SaveChangesAsync(cancellationToken);
        return crearPolizaRequest.Id;
    }
}
