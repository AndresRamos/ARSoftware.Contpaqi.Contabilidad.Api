using Api.Core.Domain.Models.Enums;

namespace Api.Core.Domain.Models;

/// <summary>
///     Model tha represents a CONTPAQi Contabilidad Movimiento
/// </summary>
public sealed class Movimiento
{
    public int Numero { get; set; }

    /// <summary>
    ///     Tipo de movimiento: Cargo = 1, Abono = 2
    /// </summary>
    public TipoMovimiento Tipo { get; set; }

    /// <summary>
    ///     Cuenta contable que afecta al movimiento.
    /// </summary>
    public Cuenta Cuenta { get; set; } = new();

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
    ///     Segmento de negocio.
    /// </summary>
    public SegmentoNegocio? SegmentoNegocio { get; set; }

    /// <summary>
    ///     Diario especial.
    /// </summary>
    public DiarioEspecial? Diario { get; set; }

    /// <summary>
    ///     UUID a asociar al movimiento.
    /// </summary>
    public string Uuid { get; set; } = string.Empty;

    /// <summary>
    ///     Datos extra del movimiento
    /// </summary>
    public Dictionary<string, string> DatosExtra { get; set; }
}
