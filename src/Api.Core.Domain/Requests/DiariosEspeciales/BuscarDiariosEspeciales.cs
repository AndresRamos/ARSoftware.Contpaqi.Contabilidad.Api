using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

public sealed class BuscarDiariosEspecialesRequest : ApiRequestBase,
    IApiRequest<BuscarDiariosEspecialesRequestModel, BuscarDiariosEspecialesRequestOptions>
{
    public BuscarDiariosEspecialesRequestModel Model { get; set; } = new();
    public BuscarDiariosEspecialesRequestOptions Options { get; set; } = new();
}

public sealed class BuscarDiariosEspecialesRequestModel
{
    public int? Id { get; set; }
    public string? Codigo { get; set; }
    public string? SqlQuery { get; set; }
}

public sealed class BuscarDiariosEspecialesRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

public sealed class BuscarDiariosEspecialesResponse : ApiResponseBase, IApiResponse<BuscarDiariosEspecialesResponseModel>
{
    public BuscarDiariosEspecialesResponseModel Model { get; set; } = new();
}

public sealed class BuscarDiariosEspecialesResponseModel
{
    public int NumeroRegistros => DiariosEspeciales.Count;
    public List<DiarioEspecial> DiariosEspeciales { get; set; } = new();
}
