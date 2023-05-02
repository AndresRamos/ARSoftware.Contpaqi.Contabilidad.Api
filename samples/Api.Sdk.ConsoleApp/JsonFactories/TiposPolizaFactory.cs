using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;

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
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_Todo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorId.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorSql.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorSql(), options));
    }

    private static BuscarTiposPolizaRequest IniciarlizarBuscarTiposPolizaRequest()
    {
        var request = new BuscarTiposPolizaRequest(new BuscarTiposPolizaRequestModel(), new BuscarTiposPolizaRequestOptions());

        return request;
    }
}
