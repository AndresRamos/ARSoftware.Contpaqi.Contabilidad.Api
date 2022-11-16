namespace Api.SharedKernel.Models;

public sealed class CreateCuentaRequest : Request
{
    public Cuenta Model { get; set; } = new();

    public static CreateCuentaRequest CreateNew(Cuenta model)
    {
        return new CreateCuentaRequest { Model = model };
    }
}
