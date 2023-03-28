namespace Api.Core.Domain.Models;

/// <summary>
///     Model that represents a CONTPAQi Contabilidad Cuenta
/// </summary>
public sealed class Cuenta
{
    /// <summary>
    ///     Número identificador de la cuenta contable.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Código de la cuenta contable.
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre de la cuenta contable.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre de la cuenta contable en otro idioma.
    /// </summary>
    public string NombreOtroIdioma { get; set; } = string.Empty;

    /// <summary>
    ///     Código de la cuenta a la que acumula la cuenta.
    /// </summary>
    public string CodigoCuentaAcumula { get; set; } = string.Empty;

    /// <summary>
    ///     Tipo de cuenta
    /// </summary>
    public CuentaTipo Tipo { get; set; }

    /// <summary>
    ///     Clave de mayor
    /// </summary>
    public CuentaDeMayor CuentaDeMayor { get; set; }

    /// <summary>
    ///     Bandera de segmento de negocio a nivel movimiento. Contiene los siguientes valores: True = Cuando en la lista de
    ///     campos se tiene seleccionadoel número de segmento de negocio, False = Cuando en la lista de campos aparece la
    ///     opción No Acepta
    /// </summary>
    public bool SegmentoNegocioEnMovimientos { get; set; }

    /// <summary>
    ///     Código del segmento de negocio al que pertenece la cuenta contable.
    /// </summary>
    public string SegmentoNegocio { get; set; } = string.Empty;

    /// <summary>
    ///     Código identificador de la moneda de la cuenta contable.
    /// </summary>
    public string Moneda { get; set; } = string.Empty;

    /// <summary>
    ///     Dígito agrupador de la cuenta contable.
    /// </summary>
    public int DigitoAgrupador { get; set; }

    /// <summary>
    ///     Código del Agrupador del SAT
    /// </summary>
    public string AgrupadorSat { get; set; } = string.Empty;

    /// <summary>
    ///     Fecha en que se registró la cuenta contable.
    /// </summary>
    public DateTime FechaAlta { get; set; }

    /// <summary>
    ///     Bandera de estado de baja de la cuenta contable. Contiene los siguientes valores: True = Cuando se habilita la
    ///     casilla Inactiva, False = Cuando no se habilita la casilla Inactiva
    /// </summary>
    public bool EsBaja { get; set; }
}
