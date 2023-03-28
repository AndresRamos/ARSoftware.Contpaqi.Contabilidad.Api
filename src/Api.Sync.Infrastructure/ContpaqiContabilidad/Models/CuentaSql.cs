namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Models;

public sealed class CuentaSql
{
    public int Id { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string NomIdioma { get; set; } = string.Empty;

    public string Tipo { get; set; } = string.Empty;

    public int? CtaMayor { get; set; }

    public bool? SegNegMovtos { get; set; }

    public int? DigAgrup { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public bool? EsBaja { get; set; }

    public int? IdSegNeg { get; set; }

    public int? IdMoneda { get; set; }

    public int? IdAgrupadorSAT { get; set; }
}
