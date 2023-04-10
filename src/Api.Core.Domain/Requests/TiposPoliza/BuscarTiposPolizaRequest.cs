using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

public sealed class BuscarTiposPolizaRequest : ApiRequestBase, IApiRequest<BuscarTiposPolizaRequestModel, BuscarTiposPolizaRequestOptions>
{
    public BuscarTiposPolizaRequestModel Model { get; set; } = new();
    public BuscarTiposPolizaRequestOptions Options { get; set; } = new();
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

public sealed class BuscarTiposPolizaResponse : ApiResponseBase, IApiResponse<BuscarTiposPolizaResponseModel>
{
    public BuscarTiposPolizaResponseModel Model { get; set; } = new();
}

public sealed class BuscarTiposPolizaResponseModel
{
    public int NumeroRegistros => TiposPoliza.Count;

    public List<TipoPoliza> TiposPoliza { get; set; } = new();
}
