using System.Text.Json.Serialization;
using Api.SharedKernel.Requests;

namespace Api.SharedKernel.Common;

[JsonDerivedType(typeof(CreatePolizaResponse), nameof(CreatePolizaResponse))]
[JsonDerivedType(typeof(CreateCuentaResponse), nameof(CreateCuentaResponse))]
public abstract class ApiResponseBase
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }

    protected virtual void SetSuccess()
    {
        IsSuccess = true;
    }

    protected virtual void SetFailed(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}
