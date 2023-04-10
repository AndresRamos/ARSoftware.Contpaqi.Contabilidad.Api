namespace Api.Core.Domain.Models;

/// <summary>
///     Modelo de la llave de una poliza.
/// </summary>
public sealed class LlavePoliza
{
    /// <summary>
    ///     Tipo de la poliza.
    /// </summary>
    public int Tipo { get; set; }

    /// <summary>
    ///     Ejercicio de la poliza.
    /// </summary>
    public int Ejercicio { get; set; }

    /// <summary>
    ///     Periodo de la poliza.
    /// </summary>
    public int Periodo { get; set; }

    /// <summary>
    ///     Numero de la poliza.
    /// </summary>
    public int Numero { get; set; }
}
