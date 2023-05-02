using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

public sealed class BuscarDiariosEspecialesRequest : ContpaqiRequest<BuscarDiariosEspecialesRequestModel,
    BuscarDiariosEspecialesRequestOptions, BuscarDiariosEspecialesResponse>
{
    public BuscarDiariosEspecialesRequest(BuscarDiariosEspecialesRequestModel model, BuscarDiariosEspecialesRequestOptions options) :
        base(model, options)
    {
    }
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

public sealed class BuscarDiariosEspecialesResponse : ContpaqiResponse<BuscarDiariosEspecialesResponseModel>
{
    public BuscarDiariosEspecialesResponse(BuscarDiariosEspecialesResponseModel model) : base(model)
    {
    }

    public static BuscarDiariosEspecialesResponse CreateInstance(List<DiarioEspecial> diariosEspeciales)
    {
        return new BuscarDiariosEspecialesResponse(new BuscarDiariosEspecialesResponseModel(diariosEspeciales));
    }
}

public sealed class BuscarDiariosEspecialesResponseModel
{
    public BuscarDiariosEspecialesResponseModel(List<DiarioEspecial> diariosEspeciales)
    {
        DiariosEspeciales = diariosEspeciales;
    }

    public int NumeroRegistros => DiariosEspeciales.Count;
    public List<DiarioEspecial> DiariosEspeciales { get; set; }
}
