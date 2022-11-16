using System.Text.Json.Serialization;

namespace Api.SharedKernel.Models;

[JsonDerivedType(typeof(CreatePolizaRequest), nameof(CreatePolizaRequest))]
[JsonDerivedType(typeof(CreateCuentaRequest), nameof(CreateCuentaRequest))]
public abstract class Request
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public bool IsProcessed { get; set; }
    public Response? Response { get; set; }
}
