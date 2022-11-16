namespace Api.SharedKernel.Models;

/// <summary>
///     Create poliza request options.
/// </summary>
public sealed class CreatePolizaOptions
{
    /// <summary>
    ///     Use the next poliza number in CONTPAQi Contabilidad. If false, poliza number assigned in the request model will be
    ///     used.
    /// </summary>
    public bool BuscarSiguienteNumero { get; set; } = true;
}
