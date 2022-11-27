using System.Text.Json.Serialization;

namespace Api.SharedKernel.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MovimientoTipo
{
    Cargo = 1,
    Abono = 2
}
