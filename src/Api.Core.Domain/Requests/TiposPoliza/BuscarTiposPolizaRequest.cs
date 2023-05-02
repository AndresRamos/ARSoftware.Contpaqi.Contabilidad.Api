using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

public sealed class
    BuscarTiposPolizaRequest : ContpaqiRequest<BuscarTiposPolizaRequestModel, BuscarTiposPolizaRequestOptions, BuscarTiposPolizaResponse>
{
    public BuscarTiposPolizaRequest(BuscarTiposPolizaRequestModel model, BuscarTiposPolizaRequestOptions options) : base(model, options)
    {
    }
}

public sealed class BuscarTiposPolizaRequestModel
{
    public int? Id { get; set; }
    public int? Codigo { get; set; }
    public string? SqlQuery { get; set; }
}

public sealed class BuscarTiposPolizaRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

public sealed class BuscarTiposPolizaResponse : ContpaqiResponse<BuscarTiposPolizaResponseModel>
{
    public BuscarTiposPolizaResponse(BuscarTiposPolizaResponseModel model) : base(model)
    {
    }

    public static BuscarTiposPolizaResponse CreateInstance(List<TipoPoliza> tiposPoliza)
    {
        return new BuscarTiposPolizaResponse(new BuscarTiposPolizaResponseModel(tiposPoliza));
    }
}

public sealed class BuscarTiposPolizaResponseModel
{
    public BuscarTiposPolizaResponseModel(List<TipoPoliza> tiposPoliza)
    {
        TiposPoliza = tiposPoliza;
    }

    public int NumeroRegistros => TiposPoliza.Count;

    public List<TipoPoliza> TiposPoliza { get; set; }
}
