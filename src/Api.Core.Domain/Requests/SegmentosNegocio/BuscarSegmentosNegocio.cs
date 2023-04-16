using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

public sealed class BuscarSegmentosNegocioRequest : ApiRequestBase,
    IApiRequest<BuscarSegmentosNegocioRequestModel, BuscarSegmentosNegocioRequestOptions>
{
    public BuscarSegmentosNegocioRequestModel Model { get; set; } = new();
    public BuscarSegmentosNegocioRequestOptions Options { get; set; } = new();
}

public sealed class BuscarSegmentosNegocioRequestModel
{
    public int? Id { get; set; }
    public string? Codigo { get; set; }
    public string? SqlQuery { get; set; }
}

public sealed class BuscarSegmentosNegocioRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

public sealed class BuscarSegmentosNegocioResponse : ApiResponseBase, IApiResponse<BuscarSegmentosNegocioResponseModel>
{
    public BuscarSegmentosNegocioResponseModel Model { get; set; } = new();
}

public sealed class BuscarSegmentosNegocioResponseModel
{
    public int NumeroRegistros => SegmentosNegocio.Count;
    public List<SegmentoNegocio> SegmentosNegocio { get; set; } = new();
}
