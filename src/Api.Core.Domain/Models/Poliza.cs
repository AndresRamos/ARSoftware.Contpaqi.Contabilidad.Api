namespace Api.Core.Domain.Models;

/// <summary>
///     Model that represents a CONTPAQi Contabilidad Poliza
/// </summary>
public sealed class Poliza
{
    /// <summary>
    ///     Id de la poliza.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Fecha de la póliza.
    /// </summary>
    public DateTime Fecha { get; set; } = DateTime.Today;

    /// <summary>
    ///     Tipo de póliza: 1 = Ingresos, 2 = Egresos, 3 = Diario, 4 = Orden, 5 = Estadística, 6 en adelante = Creadas por el
    ///     usuario
    /// </summary>
    public TipoPoliza Tipo { get; set; } = new();

    /// <summary>
    ///     Folio de la póliza.
    /// </summary>
    public int Numero { get; set; }

    /// <summary>
    ///     Concepto de la póliza.
    /// </summary>
    public string Concepto { get; set; } = string.Empty;

    /// <summary>
    ///     Movimientos contables de la poliza.
    /// </summary>
    public List<Movimiento> Movimientos { get; set; } = new();

    /// <summary>
    ///     UUIDs a asociar a la poliza.
    /// </summary>
    public List<string> Uuids { get; set; } = new();

    /// <summary>
    ///     Datos extra de la poliza.
    /// </summary>
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
