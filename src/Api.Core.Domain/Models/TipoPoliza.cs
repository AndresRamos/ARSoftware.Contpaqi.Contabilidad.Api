namespace Api.Core.Domain.Models;

/// <summary>
///     Modelo que representa un CONTPAQi Contabilidad Tipo De Poliza
/// </summary>
public sealed class TipoPoliza
{
    /// <summary>
    ///     Código del tipo de póliza.
    /// </summary>
    public int Codigo { get; set; }

    /// <summary>
    ///     Nombre del tipo de póliza.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
