using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

public sealed class
    BuscarEmpresasRequest : ContpaqiRequest<BuscarEmpresasRequestModel, BuscarEmpresasRequestOptions, BuscarEmpresasResponse>
{
    public BuscarEmpresasRequest(BuscarEmpresasRequestModel model, BuscarEmpresasRequestOptions options) : base(model, options)
    {
    }
}

public sealed class BuscarEmpresasRequestModel
{
}

public sealed class BuscarEmpresasRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

public sealed class BuscarEmpresasResponse : ContpaqiResponse<BuscarEmpresasResponseModel>
{
    public BuscarEmpresasResponse(BuscarEmpresasResponseModel model) : base(model)
    {
    }

    public static BuscarEmpresasResponse CreateInstance(List<Empresa> empresas)
    {
        return new BuscarEmpresasResponse(new BuscarEmpresasResponseModel(empresas));
    }
}

public sealed class BuscarEmpresasResponseModel
{
    public BuscarEmpresasResponseModel(List<Empresa> empresas)
    {
        Empresas = empresas;
    }

    public int NumeroRegistros => Empresas.Count;

    public List<Empresa> Empresas { get; set; }
}
