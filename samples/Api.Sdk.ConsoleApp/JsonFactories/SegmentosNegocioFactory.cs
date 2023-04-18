using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public sealed class SegmentosNegocioFactory
{
    public static BuscarSegmentosNegocioRequest BuscarPorId()
    {
        BuscarSegmentosNegocioRequest request = IniciarlizarBuscarSegmentosNegocioRequest();
        request.Model.Id = 1;
        return request;
    }

    public static BuscarSegmentosNegocioRequest BuscarPorCodigo()
    {
        BuscarSegmentosNegocioRequest request = IniciarlizarBuscarSegmentosNegocioRequest();
        request.Model.Codigo = "9999";
        return request;
    }

    public static BuscarSegmentosNegocioRequest BuscarPorSql()
    {
        BuscarSegmentosNegocioRequest request = IniciarlizarBuscarSegmentosNegocioRequest();
        request.Model.SqlQuery = "Nombre = 'Segmento por omisión'";
        return request;
    }

    public static BuscarSegmentosNegocioRequest BuscarTodo()
    {
        BuscarSegmentosNegocioRequest request = IniciarlizarBuscarSegmentosNegocioRequest();
        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarSegmentosNegocioRequest)}_Todo.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarSegmentosNegocioRequest)}_PorId.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarSegmentosNegocioRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarSegmentosNegocioRequest)}_PorSql.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorSql(), options));
    }

    private static BuscarSegmentosNegocioRequest IniciarlizarBuscarSegmentosNegocioRequest()
    {
        var request = new BuscarSegmentosNegocioRequest();

        return request;
    }
}
