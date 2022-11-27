using Api.SharedKernel.Models;
using FluentValidation;

namespace Api.Core.Application.Polizas.Validators;

public sealed class CreatePolizaModelValidator : AbstractValidator<Poliza>
{
    public CreatePolizaModelValidator()
    {
        RuleFor(m => m.Tipo).NotEmpty();
        RuleFor(m => m.Fecha).NotEmpty();
        RuleFor(m => m.Concepto).NotEmpty();
        RuleFor(m => m.Movimientos).NotEmpty().Must(m => m.Count > 0).WithMessage("Poliza should have at least one movimiento.");

        RuleForEach(m => m.Movimientos).SetValidator(new CreateMovimientoModelValidator());
    }
}
