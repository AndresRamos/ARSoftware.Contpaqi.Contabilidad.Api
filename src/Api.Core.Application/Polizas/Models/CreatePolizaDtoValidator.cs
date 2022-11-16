using Api.SharedKernel.Models;
using FluentValidation;

namespace Api.Core.Application.Polizas.Models;

public sealed class CreatePolizaDtoValidator : AbstractValidator<Poliza>
{
    public CreatePolizaDtoValidator()
    {
        RuleFor(m => m.Tipo).NotEmpty();
        RuleFor(m => m.Fecha).NotEmpty();
        RuleFor(m => m.Concepto).NotEmpty();
        RuleFor(m => m.Movimientos).NotEmpty().Must(m => m.Count > 0).WithMessage("Polizas should have at least one movimiento.");

        RuleForEach(m => m.Movimientos).SetValidator(new CreateMovimientoDtoValidator());
    }
}
