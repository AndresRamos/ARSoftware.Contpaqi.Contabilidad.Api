using Api.SharedKernel.Models;
using MediatR;

namespace Api.Core.Application.Cuentas.Commands;

public sealed class CreateCuentaCommand : IRequest<Guid>
{
    public CreateCuentaCommand(Cuenta model)
    {
        Model = model;
    }

    public Cuenta Model { get; }
}
