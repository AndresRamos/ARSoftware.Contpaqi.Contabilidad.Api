namespace Api.SharedKernel.Models;

public sealed class CreatePolizaRequest : Request
{
    public CreatePolizaOptions Options { get; set; }
    public Poliza Model { get; set; }
}
