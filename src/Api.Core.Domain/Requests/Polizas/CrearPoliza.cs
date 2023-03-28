using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

public sealed class CrearPolizaRequest : ApiRequestBase, IApiRequest<CrearPolizaRequestModel, CrearPolizaRequestOptions>
{
    public CrearPolizaRequestModel Model { get; set; } = new();
    public CrearPolizaRequestOptions Options { get; set; } = new();
}

public sealed class CrearPolizaRequestModel
{
    public Poliza Poliza { get; set; } = new();
}

public sealed class CrearPolizaRequestOptions : ILoadRelatedDataOptions
{
    public bool BuscarSiguienteNumero { get; set; } = true;
    public bool CargarDatosExtra { get; set; }
}

public sealed class CrearPolizaResponse : ApiResponseBase, IApiResponse<CrearPolizaResponseModel>
{
    public CrearPolizaResponseModel Model { get; set; } = new();
}

public sealed class CrearPolizaResponseModel
{
    public Poliza? Poliza { get; set; }
}
