using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

public sealed class BuscarCuentasRequest : ContpaqiRequest<BuscarCuentasRequestModel, BuscarCuentasRequestOptions, BuscarCuentasResponse>
{
    public BuscarCuentasRequest(BuscarCuentasRequestModel model, BuscarCuentasRequestOptions options) : base(model, options)
    {
    }
}

public sealed class BuscarCuentasRequestModel
{
    public int? Id { get; set; }
    public string? Codigo { get; set; }
    public string? SqlQuery { get; set; }
}

public sealed class BuscarCuentasRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

public sealed class BuscarCuentasResponse : ContpaqiResponse<BuscarCuentasResponseModel>
{
    public BuscarCuentasResponse(BuscarCuentasResponseModel model) : base(model)
    {
    }

    public static BuscarCuentasResponse CreateInstance(List<Cuenta> cuentas)
    {
        return new BuscarCuentasResponse(new BuscarCuentasResponseModel(cuentas));
    }
}

public sealed class BuscarCuentasResponseModel
{
    public BuscarCuentasResponseModel(List<Cuenta> cuentas)
    {
        Cuentas = cuentas;
    }

    public List<Cuenta> Cuentas { get; set; }
}
