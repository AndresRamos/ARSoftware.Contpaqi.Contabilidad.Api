using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar polizas.
/// </summary>
public sealed class EliminarPolizaRequest : IContpaqiRequest<EliminarPolizaRequestModel, EliminarPolizaRequestOptions>
{
    public EliminarPolizaRequestModel Model { get; set; } = new();
    public EliminarPolizaRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud EliminarPolizaRequest.
/// </summary>
public sealed class EliminarPolizaRequestModel
{
    public LlavePoliza LlavePoliza { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud EliminarPolizaRequest.
/// </summary>
public sealed class EliminarPolizaRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud EliminarPolizaRequest.
/// </summary>
public sealed class EliminarPolizaResponse : IContpaqiResponse<EliminarPolizaResponseModel>
{
    public EliminarPolizaResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta EliminarPolizaResponse.
/// </summary>
public sealed class EliminarPolizaResponseModel
{
}
