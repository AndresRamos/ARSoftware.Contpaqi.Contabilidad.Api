using Api.SharedKernel.Common;
using Api.SharedKernel.Models;

namespace Api.SharedKernel.Requests;

public sealed class CreatePolizaResponse : ApiResponseBase
{
    public Poliza? Model { get; set; }

    public static CreatePolizaResponse CreateSuccess(Poliza poliza)
    {
        var response = new CreatePolizaResponse();
        response.SetSuccess();
        response.Model = poliza;
        return response;
    }

    public static CreatePolizaResponse CreateFailed(string errorMessage)
    {
        var response = new CreatePolizaResponse();
        response.SetFailed(errorMessage);
        return response;
    }
}
