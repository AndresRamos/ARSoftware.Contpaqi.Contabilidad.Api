using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

public sealed class BuscarSegmentosNegocioRequest : ContpaqiRequest<BuscarSegmentosNegocioRequestModel, BuscarSegmentosNegocioRequestOptions
    , BuscarSegmentosNegocioResponse>
{
    public BuscarSegmentosNegocioRequest(BuscarSegmentosNegocioRequestModel model, BuscarSegmentosNegocioRequestOptions options) :
        base(model, options)
    {
    }
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

public sealed class BuscarSegmentosNegocioResponse : ContpaqiResponse<BuscarSegmentosNegocioResponseModel>
{
    public BuscarSegmentosNegocioResponse(BuscarSegmentosNegocioResponseModel model) : base(model)
    {
    }

    public static BuscarSegmentosNegocioResponse CreateInstance(List<SegmentoNegocio> segmentosNegocio)
    {
        return new BuscarSegmentosNegocioResponse(new BuscarSegmentosNegocioResponseModel(segmentosNegocio));
    }
}

public sealed class BuscarSegmentosNegocioResponseModel
{
    public BuscarSegmentosNegocioResponseModel(List<SegmentoNegocio> segmentosNegocio)
    {
        SegmentosNegocio = segmentosNegocio;
    }

    public int NumeroRegistros => SegmentosNegocio.Count;
    public List<SegmentoNegocio> SegmentosNegocio { get; set; }
}
