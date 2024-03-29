﻿using System.Text.Json.Serialization;

namespace Api.Core.Domain.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoCuenta
{
    ActivoDeudora = 1,
    ActivoAcreedora = 2,
    PasivoDeudora = 3,
    PasivoAcreedora = 4,
    CapitalDeudora = 5,
    CapitalAcreedora = 6,
    ResultadosDeudora = 7,
    ResultadosAcreedora = 8,
    EstadisticasDeudora = 9,
    EstadisticasAcreedora = 10,
    OrdenDeudora = 11,
    OrdenAcreedora = 12
}
