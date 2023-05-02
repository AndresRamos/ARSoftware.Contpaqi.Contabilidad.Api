using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

public sealed class CrearCuentaRequest : ContpaqiRequest<CrearCuentaRequestModel, CrearCuentaRequestOptions, CrearCuentaResponse>
{
    public CrearCuentaRequest(CrearCuentaRequestModel model, CrearCuentaRequestOptions options) : base(model, options)
    {
    }
}

public sealed class CrearCuentaRequestModel
{
    public Cuenta Cuenta { get; set; } = new();
}

public sealed class CrearCuentaRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

public sealed class CrearCuentaResponse : ContpaqiResponse<CrearCuentaResponseModel>
{
    public CrearCuentaResponse(CrearCuentaResponseModel model) : base(model)
    {
    }

    public static CrearCuentaResponse CreateInstance(Cuenta cuenta)
    {
        return new CrearCuentaResponse(new CrearCuentaResponseModel(cuenta));
    }
}

public sealed class CrearCuentaResponseModel
{
    public CrearCuentaResponseModel(Cuenta cuenta)
    {
        Cuenta = cuenta;
    }

    public Cuenta Cuenta { get; set; }
}
