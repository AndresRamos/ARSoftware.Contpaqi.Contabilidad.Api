using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class PolizaFactory
{
    public static CrearPolizaRequest Crear()
    {
        var request = new CrearPolizaRequest();
        request.EmpresaRfc = "URE180429TM6";

        request.Model.Poliza = new Poliza
        {
            Tipo = "3",
            Concepto = "Concepto Poliza",
            Fecha = DateTime.Today,
            Movimientos = new List<Movimiento>
            {
                new()
                {
                    Numero = 1,
                    Tipo = MovimientoTipo.Abono,
                    Cuenta = "60101000",
                    Importe = 100,
                    Referencia = "Referencia",
                    Concepto = "Concepto"
                },
                new()
                {
                    Numero = 2,
                    Tipo = MovimientoTipo.Cargo,
                    Cuenta = "40119000",
                    Importe = 100,
                    Referencia = "Referencia",
                    Concepto = "Concepto"
                }
            }
        };

        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearPolizaRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Crear(), options));
    }
}
