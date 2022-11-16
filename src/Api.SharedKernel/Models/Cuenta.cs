namespace Api.SharedKernel.Models;

public sealed class Cuenta
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string NombreOtroIdioma { get; set; } = string.Empty;
    public string CodigoCuentaAcumula { get; set; } = string.Empty;
    public CuentaTipo Tipo { get; set; }
    public CuentaDeMayor CuentaDeMayor { get; set; }
    public bool SegmentoNegocioEnMovimientos { get; set; }
    public string SegmentoNegocio { get; set; }
    public string Moneda { get; set; }
    public int DigitoAgrupador { get; set; }
    public string AgrupadorSat { get; set; } = string.Empty;
    public DateTime FechaAlta { get; set; }
    public bool EsBaja { get; set; }
}
