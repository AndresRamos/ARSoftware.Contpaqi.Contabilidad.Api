using Api.Core.Application.Common;
using Api.SharedKernel.Requests;
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
        CreateCuentaRequest apiRequest = request.ApiRequest;

        var createCuentaRequest = CreateCuentaRequest.CreateNew(apiRequest.Model);

        _context.Requests.Add(createCuentaRequest);

        await _context.SaveChangesAsync(cancellationToken);

        return createCuentaRequest.Id;
    }
}
