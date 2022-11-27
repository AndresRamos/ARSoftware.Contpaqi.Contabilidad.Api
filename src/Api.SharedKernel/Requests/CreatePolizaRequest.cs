using Api.SharedKernel.Common;
using Api.SharedKernel.Models;

namespace Api.SharedKernel.Requests;

/// <summary>
///     Request to create a poliza
/// </summary>
public sealed class CreatePolizaRequest : ApiRequestBase
{
    /// <summary>
    ///     Request model.
    /// </summary>
    public Poliza Model { get; set; } = new();

    /// <summary>
    ///     Request options.
    /// </summary>
    public CreatePolizaOptions Options { get; set; } = new();
}
