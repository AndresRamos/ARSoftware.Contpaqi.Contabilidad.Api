using System.Text.Json.Serialization;

namespace Api.SharedKernel.Models;

[JsonDerivedType(typeof(CreatePolizaResponse), nameof(CreatePolizaResponse))]
[JsonDerivedType(typeof(CreateCuentaResponse), nameof(CreateCuentaResponse))]
public abstract class Response
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
}
