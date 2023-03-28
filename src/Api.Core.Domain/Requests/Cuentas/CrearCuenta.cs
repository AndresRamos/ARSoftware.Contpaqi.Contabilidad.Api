using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

public sealed class CrearCuentaRequest : ApiRequestBase, IApiRequest<CrearCuentaRequestModel, CrearCuentaRequestOptions>
{
    public CrearCuentaRequestModel Model { get; set; } = new();
    public CrearCuentaRequestOptions Options { get; set; } = new();
}

public sealed class CrearCuentaRequestModel
{
    public Cuenta Cuenta { get; set; } = new();
}

public sealed class CrearCuentaRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

public sealed class CrearCuentaResponse : ApiResponseBase, IApiResponse<CrearCuentaResponseModel>
{
    public CrearCuentaResponseModel Model { get; set; } = new();
}

public sealed class CrearCuentaResponseModel
{
    public Cuenta? Cuenta { get; set; }
}
