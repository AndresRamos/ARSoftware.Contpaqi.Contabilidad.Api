using Api.SharedKernel.Models;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Extensions;

public static class ContabilidadSdkExtensions
{
    public static CuentaTipo ConvertToCuentaTipo(string dbValue)
    {
        return dbValue switch
        {
            "A" => CuentaTipo.ActivoDeudora,
            "B" => CuentaTipo.ActivoAcreedora,
            "C" => CuentaTipo.PasivoDeudora,
            "D" => CuentaTipo.PasivoAcreedora,
            "E" => CuentaTipo.CapitalDeudora,
            "F" => CuentaTipo.CapitalAcreedora,
            "G" => CuentaTipo.ResultadosDeudora,
            "H" => CuentaTipo.ResultadosAcreedora,
            "I" => CuentaTipo.EstadisticasDeudora,
            "J" => CuentaTipo.EstadisticasAcreedora,
            "K" => CuentaTipo.OrdenDeudora,
            "L" => CuentaTipo.OrdenAcreedora,
            _ => throw new NotImplementedException($"DB Value {dbValue} is not a valid {typeof(CuentaTipo)} value.")
        };
    }

    public static CuentaDeMayor ConvertToCuentaDeMayor(int? dbValue)
    {
        return dbValue switch
        {
            1 => CuentaDeMayor.Si,
            2 => CuentaDeMayor.No,
            3 => CuentaDeMayor.DeTitulo,
            4 => CuentaDeMayor.DeSubtitulo,
            _ => throw new NotImplementedException($"DB Value {dbValue} is not a valid {typeof(CuentaDeMayor)} value.")
        };
    }
}
