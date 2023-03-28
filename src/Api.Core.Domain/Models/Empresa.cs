namespace Api.Core.Domain.Models;

public sealed class Empresa
{
    public string Nombre { get; set; } = string.Empty;
    public string BaseDatos { get; set; } = string.Empty;
    public string Rfc { get; set; }
    public string GuidAdd { get; set; } = string.Empty;
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
