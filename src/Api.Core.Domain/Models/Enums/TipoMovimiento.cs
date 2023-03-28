using System.Text.Json.Serialization;

namespace Api.Core.Domain.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoMovimiento
{
    Cargo = 1,
    Abono = 2
}
