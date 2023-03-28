using System.ComponentModel;
using Api.Core.Domain.Models.Enums;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Extensions;

public static class ContabilidadSdkExtensions
{
    public static TipoCuenta ConvertToCuentaTipo(string dbValue)
    {
        return dbValue switch
        {
            "A" => TipoCuenta.ActivoDeudora,
            "B" => TipoCuenta.ActivoAcreedora,
            "C" => TipoCuenta.PasivoDeudora,
            "D" => TipoCuenta.PasivoAcreedora,
            "E" => TipoCuenta.CapitalDeudora,
            "F" => TipoCuenta.CapitalAcreedora,
            "G" => TipoCuenta.ResultadosDeudora,
            "H" => TipoCuenta.ResultadosAcreedora,
            "I" => TipoCuenta.EstadisticasDeudora,
            "J" => TipoCuenta.EstadisticasAcreedora,
            "K" => TipoCuenta.OrdenDeudora,
            "L" => TipoCuenta.OrdenAcreedora,
            _ => throw new NotImplementedException($"DB Value {dbValue} is not a valid {typeof(TipoCuenta)} value.")
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

    public static Dictionary<string, string> ToDatosDictionary<TModel>(this object model)
    {
        var datosDictionary = new Dictionary<string, string>();

        Type sqlModelType = typeof(TModel);

        foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(model))
        {
            if (!sqlModelType.HasProperty(propertyDescriptor.Name) || propertyDescriptor.Name == "CTIMESTAMP")
                continue;

            if (propertyDescriptor.GetValue(model)?.ToString() is null)
                continue;

            datosDictionary.Add(propertyDescriptor.Name, propertyDescriptor.GetValue(model)?.ToString());
        }

        return datosDictionary;
    }

    public static bool HasProperty(this Type obj, string propertyName)
    {
        return obj.GetProperty(propertyName) != null;
    }
}
