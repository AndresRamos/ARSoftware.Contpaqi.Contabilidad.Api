using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar polizas.
/// </summary>
public sealed class
    EliminarPolizaRequest : ContpaqiRequest<EliminarPolizaRequestModel, EliminarPolizaRequestOptions, EliminarPolizaResponse>
{
    public EliminarPolizaRequest(EliminarPolizaRequestModel model, EliminarPolizaRequestOptions options) : base(model, options)
    {
    }
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
public sealed class EliminarPolizaResponse : ContpaqiResponse<EliminarPolizaResponseModel>
{
    public EliminarPolizaResponse(EliminarPolizaResponseModel model) : base(model)
    {
    }

    public static EliminarPolizaResponse CreateInstance()
    {
        return new EliminarPolizaResponse(new EliminarPolizaResponseModel());
    }
}

/// <summary>
///     Modelo de la respuesta EliminarPolizaResponse.
/// </summary>
public sealed class EliminarPolizaResponseModel
{
}
