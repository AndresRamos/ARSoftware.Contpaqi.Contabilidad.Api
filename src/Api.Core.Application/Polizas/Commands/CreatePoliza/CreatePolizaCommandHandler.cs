using Api.Core.Application.Common;
using Api.SharedKernel.Requests;
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
        var crearPolizaRequest = new CreatePolizaRequest
        {
            DateCreated = DateTime.Now,
            Model = request.ApiRequest.Model,
            Options = request.ApiRequest.Options
        };
        _context.Requests.Add(crearPolizaRequest);

        await _context.SaveChangesAsync(cancellationToken);
        return crearPolizaRequest.Id;
    }
}
