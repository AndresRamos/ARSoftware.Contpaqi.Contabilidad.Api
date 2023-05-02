using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar polizas.
/// </summary>
public sealed class BuscarPolizasRequest : ContpaqiRequest<BuscarPolizasRequestModel, BuscarPolizasRequestOptions, BuscarPolizasResponse>
{
    public BuscarPolizasRequest(BuscarPolizasRequestModel model, BuscarPolizasRequestOptions options) : base(model, options)
    {
    }
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
public sealed class BuscarPolizasResponse : ContpaqiResponse<BuscarPolizasResponseModel>
{
    public BuscarPolizasResponse(BuscarPolizasResponseModel model) : base(model)
    {
    }

    public static BuscarPolizasResponse CreateInstance(List<Poliza> polizas)
    {
        return new BuscarPolizasResponse(new BuscarPolizasResponseModel(polizas));
    }
}

/// <summary>
///     Modelo de la solicitud BuscarPolizasResponse.
/// </summary>
public sealed class BuscarPolizasResponseModel
{
    public BuscarPolizasResponseModel(List<Poliza> polizas)
    {
        Polizas = polizas;
    }

    /// <summary>
    ///     Lista de polizas encontradas.
    /// </summary>
    public List<Poliza> Polizas { get; set; }
}
