namespace Api.Core.Domain.Models;

public sealed class AgrupadorSat
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
