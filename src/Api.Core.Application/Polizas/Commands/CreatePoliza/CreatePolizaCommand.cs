using Api.SharedKernel.Models;
using MediatR;

namespace Api.Core.Application.Polizas.Commands.CreatePoliza;

/// <summary>
///     Command to create a new poliza.
/// </summary>
public sealed class CreatePolizaCommand : IRequest<Guid>
{
    public CreatePolizaCommand(Poliza model, CreatePolizaOptions options)
    {
        Model = model;
        Options = options;
    }

    /// <summary>
    ///     Poliza model.
    /// </summary>
    public Poliza Model { get; }

    /// <summary>
    ///     Create poliza command options.
    /// </summary>
    public CreatePolizaOptions Options { get; }
}
