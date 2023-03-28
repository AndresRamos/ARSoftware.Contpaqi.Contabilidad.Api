using System.Text.Json.Serialization;

namespace Api.Core.Domain.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MovimientoTipo
{
    Cargo = 1,
    Abono = 2
}
