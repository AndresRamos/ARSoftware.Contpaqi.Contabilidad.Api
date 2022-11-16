namespace Api.SharedKernel.Models;

public sealed class CreateCuentaResponse : Response
{
    public Cuenta? Model { get; set; }

    public static CreateCuentaResponse CreateSuccess(Cuenta cuenta)
    {
        var response = new CreateCuentaResponse();
        response.IsSuccess = true;
        response.Model = cuenta;
        return response;
    }

    public static CreateCuentaResponse CreateFailed(string errorMessage)
    {
        var response = new CreateCuentaResponse();
        response.IsSuccess = false;
        response.ErrorMessage = errorMessage;
        return response;
    }
}
