namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Models;

public sealed class MovimientoSql
{
    public int Id { get; set; }

    public int IdPoliza { get; set; }

    public int NumMovto { get; set; }

    public int IdCuenta { get; set; }

    public bool? TipoMovto { get; set; }

    public decimal? Importe { get; set; }

    public string Referencia { get; set; } = string.Empty;

    public string Concepto { get; set; } = string.Empty;

    public int? IdDiario { get; set; }

    public int? IdSegNeg { get; set; }
}
