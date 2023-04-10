using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public sealed class TiposPolizaFactory
{
    private static BuscarTiposPolizaRequest BuscarPorId()
    {
        BuscarTiposPolizaRequest request = IniciarlizarBuscarTiposPolizaRequest();
        request.Model.Id = 1;

        return request;
    }

    private static BuscarTiposPolizaRequest BuscarPorCodigo()
    {
        BuscarTiposPolizaRequest request = IniciarlizarBuscarTiposPolizaRequest();
        request.Model.Codigo = 2;

        return request;
    }

    private static BuscarTiposPolizaRequest BuscarPorSql()
    {
        BuscarTiposPolizaRequest request = IniciarlizarBuscarTiposPolizaRequest();
        request.Model.SqlQuery = "Nombre = 'Diario'";

        return request;
    }

    private static BuscarTiposPolizaRequest BuscarTodo()
    {
        BuscarTiposPolizaRequest request = IniciarlizarBuscarTiposPolizaRequest();
        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_Todo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorId.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorSql.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorSql(), options));
    }

    private static BuscarTiposPolizaRequest IniciarlizarBuscarTiposPolizaRequest()
    {
        var request = new BuscarTiposPolizaRequest();
        request.EmpresaRfc = "URE180429TM6";

        return request;
    }
}
