using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

public sealed class BuscarEmpresasRequest : IContpaqiRequest<BuscarEmpresasRequestModel, BuscarEmpresasRequestOptions>
{
    public BuscarEmpresasRequestModel Model { get; set; } = new();
    public BuscarEmpresasRequestOptions Options { get; set; } = new();
}

public sealed class BuscarEmpresasRequestModel
{
}

public sealed class BuscarEmpresasRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

public sealed class BuscarEmpresasResponse : IContpaqiResponse<BuscarEmpresasResponseModel>
{
    public BuscarEmpresasResponseModel Model { get; set; } = new();
}

public sealed class BuscarEmpresasResponseModel
{
    public int NumeroRegistros => Empresas.Count;

    public List<Empresa> Empresas { get; set; } = new();
}
