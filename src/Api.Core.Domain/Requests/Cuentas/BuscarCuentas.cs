using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

public sealed class BuscarCuentasRequest : IContpaqiRequest<BuscarCuentasRequestModel, BuscarCuentasRequestOptions>
{
    public BuscarCuentasRequestModel Model { get; set; } = new();
    public BuscarCuentasRequestOptions Options { get; set; } = new();
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

public sealed class BuscarCuentasResponse : IContpaqiResponse<BuscarCuentasResponseModel>
{
    public BuscarCuentasResponseModel Model { get; set; } = new();
}

public sealed class BuscarCuentasResponseModel
{
    public List<Cuenta> Cuentas { get; set; } = new();
}
