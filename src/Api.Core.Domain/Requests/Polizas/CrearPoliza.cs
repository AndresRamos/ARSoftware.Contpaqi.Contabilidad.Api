using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear una poliza.
/// </summary>
public sealed class CrearPolizaRequest : ContpaqiRequest<CrearPolizaRequestModel, CrearPolizaRequestOptions, CrearPolizaResponse>
{
    public CrearPolizaRequest(CrearPolizaRequestModel model, CrearPolizaRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud CrearPolizaRequest
/// </summary>
public sealed class CrearPolizaRequestModel
{
    public Poliza Poliza { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearPolizaRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearPolizaRequestOptions : ILoadRelatedDataOptions
{
    /// <summary>
    ///     Buscar el numero consecutivo del tipo de poliza.
    /// </summary>
    public bool BuscarSiguienteNumero { get; set; } = true;

    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearPolizaRequest.
/// </summary>
public sealed class CrearPolizaResponse : ContpaqiResponse<CrearPolizaResponseModel>
{
    public CrearPolizaResponse(CrearPolizaResponseModel model) : base(model)
    {
    }

    public static CrearPolizaResponse CreateInstance(Poliza poliza)
    {
        return new CrearPolizaResponse(new CrearPolizaResponseModel(poliza));
    }
}

/// <summary>
///     Modelo de la respuesta CrearPolizaResponse.
/// </summary>
public sealed class CrearPolizaResponseModel
{
    public CrearPolizaResponseModel(Poliza poliza)
    {
        Poliza = poliza;
    }

    public Poliza Poliza { get; set; }
}
