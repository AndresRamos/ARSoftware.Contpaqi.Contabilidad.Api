using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear una poliza.
/// </summary>
public sealed class CrearPolizaRequest : ApiRequestBase, IApiRequest<CrearPolizaRequestModel, CrearPolizaRequestOptions>
{
    public CrearPolizaRequestModel Model { get; set; } = new();
    public CrearPolizaRequestOptions Options { get; set; } = new();
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
public sealed class CrearPolizaResponse : ApiResponseBase, IApiResponse<CrearPolizaResponseModel>
{
    public CrearPolizaResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta CrearPolizaResponse.
/// </summary>
public sealed class CrearPolizaResponseModel
{
    public Poliza? Poliza { get; set; }
}
