using System.Text.Json.Serialization;
using Api.SharedKernel.Requests;

namespace Api.SharedKernel.Common;

[JsonDerivedType(typeof(CreatePolizaRequest), nameof(CreatePolizaRequest))]
[JsonDerivedType(typeof(CreateCuentaRequest), nameof(CreateCuentaRequest))]
public abstract class ApiRequestBase
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public bool IsProcessed { get; set; }
    public ApiResponseBase? Response { get; set; }
}
