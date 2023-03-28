namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Models;

public sealed class MonedaSql
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string CodigoSAT { get; set; } = string.Empty;
}
