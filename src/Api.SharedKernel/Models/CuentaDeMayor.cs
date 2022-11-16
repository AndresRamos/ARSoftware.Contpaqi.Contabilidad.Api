using System.Text.Json.Serialization;

namespace Api.SharedKernel.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CuentaDeMayor
{
    Si = 1,
    No = 2,
    DeTitulo = 3,
    DeSubtitulo = 4
}
