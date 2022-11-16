namespace Api.SharedKernel.Models;

/// <summary>
///     Model tha represents a CONTPAQi Contabilidad Poliza
/// </summary>
public sealed class Poliza
{
    /// <summary>
    ///     Número identificador de la póliza.
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
    public string Tipo { get; set; } = string.Empty;

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
}
