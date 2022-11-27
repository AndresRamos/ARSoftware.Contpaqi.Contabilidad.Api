using Api.SharedKernel.Models;
using FluentValidation;

namespace Api.Core.Application.Polizas.Validators;

public sealed class CreateMovimientoModelValidator : AbstractValidator<Movimiento>
{
    public CreateMovimientoModelValidator()
    {
        RuleFor(m => m.Numero).NotEmpty();
        RuleFor(m => m.Cuenta).NotEmpty();
        RuleFor(m => m.Importe).NotEmpty();
        RuleFor(m => m.Referencia).NotNull();
        RuleFor(m => m.Concepto).NotNull();
        RuleFor(m => m.SegmentoNegocio).NotNull();
        RuleFor(m => m.Diario).NotNull();
        RuleFor(m => m.Uuid).NotNull();
    }
}
