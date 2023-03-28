namespace Api.Core.Domain.Models;

public sealed class DiarioEspecial
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public int? Tipo { get; set; }
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
