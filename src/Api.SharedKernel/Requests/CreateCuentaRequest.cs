using Api.SharedKernel.Common;
using Api.SharedKernel.Models;

namespace Api.SharedKernel.Requests;

/// <summary>
///     Request to create a cuenta.
/// </summary>
public sealed class CreateCuentaRequest : ApiRequestBase
{
    /// <summary>
    ///     Request model.
    /// </summary>
    public Cuenta Model { get; set; } = new();

    public static CreateCuentaRequest CreateNew(Cuenta model)
    {
        return new CreateCuentaRequest { Model = model };
    }
}
