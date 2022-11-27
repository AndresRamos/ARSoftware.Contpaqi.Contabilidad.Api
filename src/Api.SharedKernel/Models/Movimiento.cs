namespace Api.SharedKernel.Models;

/// <summary>
///     Model tha represents a CONTPAQi Contabilidad Movimiento
/// </summary>
public sealed class Movimiento
{
    /// <summary>
    ///     Número identificador del movimiento.
    /// </summary>
    public int Id { get; set; }

    public int Numero { get; set; }

    /// <summary>
    ///     Tipo de movimiento: Cargo = 1, Abono = 2
    /// </summary>
    public MovimientoTipo Tipo { get; set; }

    /// <summary>
    ///     Código identificador de la cuenta contable que afecta al movimiento.
    /// </summary>
    public string Cuenta { get; set; } = string.Empty;

    /// <summary>
    ///     Importe del movimiento.
    /// </summary>
    public decimal Importe { get; set; }

    /// <summary>
    ///     Referencia del movimiento.
    /// </summary>
    public string Referencia { get; set; } = string.Empty;

    /// <summary>
    ///     Concepto del movimiento.
    /// </summary>
    public string Concepto { get; set; } = string.Empty;

    /// <summary>
    ///     Código identificador del segmento de negocio.
    /// </summary>
    public string SegmentoNegocio { get; set; } = string.Empty;

    /// <summary>
    ///     Código identificador del diario especial.
    /// </summary>
    public string Diario { get; set; } = string.Empty;

    /// <summary>
    ///     UUID a asociar al movimiento.
    /// </summary>
    public string Uuid { get; set; } = string.Empty;
}
