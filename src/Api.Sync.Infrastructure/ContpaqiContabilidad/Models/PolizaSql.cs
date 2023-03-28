namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Models;

public sealed class PolizaSql
{
    public int Id { get; set; }
    public int TipoPol { get; set; }
    public DateTime? Fecha { get; set; }
    public int Folio { get; set; }
    public string Concepto { get; set; } = string.Empty;
}
