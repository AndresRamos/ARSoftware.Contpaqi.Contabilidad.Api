using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar polizas.
/// </summary>
public sealed class BuscarPolizasRequest : ApiRequestBase, IApiRequest<BuscarPolizasRequestModel, BuscarPolizasRequestOptions>
{
    public BuscarPolizasRequestModel Model { get; set; } = new();
    public BuscarPolizasRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarPolizasRequest.
/// </summary>
public sealed class BuscarPolizasRequestModel
{
    /// <summary>
    ///     Parametro para buscar polizas por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar por rango de fecha.
    /// </summary>
    public DateOnly? FechaInicio { get; set; }

    /// <summary>
    ///     Parametro para buscar por rango de fecha.
    /// </summary>
    public DateOnly? FechaFin { get; set; }

    /// <summary>
    ///     Parametro para buscar polizas por tipo.
    /// </summary>
    public int? Tipo { get; set; }

    /// <summary>
    ///     Parametro para buscar polizas por ejercicio.
    /// </summary>
    public int? Ejercicio { get; set; }

    /// <summary>
    ///     Parametro para buscar polizas por periodo.
    /// </summary>
    public int? Periodo { get; set; }

    /// <summary>
    ///     Parametro para buscar polizas por numero.
    /// </summary>
    public int? Numero { get; set; }

    /// <summary>
    ///     Parametro para buscar polizs por SQL.
    /// </summary>
    public string? SqlQuery { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarPolizasRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarPolizasRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarPolizasRequest.
/// </summary>
public sealed class BuscarPolizasResponse : ApiResponseBase, IApiResponse<BuscarPolizasResponseModel>
{
    public BuscarPolizasResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarPolizasResponse.
/// </summary>
public sealed class BuscarPolizasResponseModel
{
    /// <summary>
    ///     Lista de polizas encontradas.
    /// </summary>
    public List<Poliza> Polizas { get; set; } = new();
}
