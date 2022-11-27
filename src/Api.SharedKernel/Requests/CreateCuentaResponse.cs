using Api.SharedKernel.Common;
using Api.SharedKernel.Models;

namespace Api.SharedKernel.Requests;

public sealed class CreateCuentaResponse : ApiResponseBase
{
    public Cuenta? Model { get; set; }

    public static CreateCuentaResponse CreateSuccess(Cuenta cuenta)
    {
        var response = new CreateCuentaResponse();
        response.SetSuccess();
        response.Model = cuenta;
        return response;
    }

    public static CreateCuentaResponse CreateFailed(string errorMessage)
    {
        var response = new CreateCuentaResponse();
        response.SetFailed(errorMessage);
        return response;
    }
}
