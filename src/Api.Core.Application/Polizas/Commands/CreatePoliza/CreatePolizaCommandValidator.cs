using Api.Core.Application.Polizas.Models;
using FluentValidation;

namespace Api.Core.Application.Polizas.Commands.CreatePoliza;

public sealed class CreatePolizaCommandValidator : AbstractValidator<CreatePolizaCommand>
{
    public CreatePolizaCommandValidator()
    {
        RuleFor(m => m.Model).SetValidator(new CreatePolizaDtoValidator());
    }
}
