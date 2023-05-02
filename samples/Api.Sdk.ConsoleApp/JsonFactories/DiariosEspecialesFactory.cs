using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public sealed class DiariosEspecialesFactory
{
    public static BuscarDiariosEspecialesRequest BuscarPorId()
    {
        BuscarDiariosEspecialesRequest request = IniciarlizarBuscarDiariosEspecialesRequest();
        request.Model.Id = 2;
        return request;
    }

    public static BuscarDiariosEspecialesRequest BuscarPorCodigo()
    {
        BuscarDiariosEspecialesRequest request = IniciarlizarBuscarDiariosEspecialesRequest();
        request.Model.Codigo = "82";
        return request;
    }

    public static BuscarDiariosEspecialesRequest BuscarPorSql()
    {
        BuscarDiariosEspecialesRequest request = IniciarlizarBuscarDiariosEspecialesRequest();
        request.Model.SqlQuery = "Nombre = 'Tasa IVA 16%'";
        return request;
    }

    public static BuscarDiariosEspecialesRequest BuscarTodo()
    {
        BuscarDiariosEspecialesRequest request = IniciarlizarBuscarDiariosEspecialesRequest();
        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDiariosEspecialesRequest)}_Todo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDiariosEspecialesRequest)}_PorId.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDiariosEspecialesRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDiariosEspecialesRequest)}_PorSql.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorSql(), options));
    }

    private static BuscarDiariosEspecialesRequest IniciarlizarBuscarDiariosEspecialesRequest()
    {
        var request = new BuscarDiariosEspecialesRequest(new BuscarDiariosEspecialesRequestModel(),
            new BuscarDiariosEspecialesRequestOptions());

        return request;
    }
}
