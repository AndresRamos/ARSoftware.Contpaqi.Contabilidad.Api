using Api.SharedKernel.Models;
using FluentValidation;

namespace Api.Core.Application.Polizas.Models;

public sealed class CreateMovimientoDtoValidator : AbstractValidator<Movimiento>
{
    public CreateMovimientoDtoValidator()
    {
        RuleFor(m => m.Numero).NotEmpty();
        RuleFor(m => m.Tipo).NotEmpty();
        RuleFor(m => m.Cuenta).NotEmpty();
        RuleFor(m => m.Importe).NotEmpty();
        RuleFor(m => m.Referencia).NotNull();
        RuleFor(m => m.Concepto).NotNull();
        RuleFor(m => m.SegmentoNegocio).NotNull();
        RuleFor(m => m.Diario).NotNull();
        RuleFor(m => m.Uuid).NotNull();
    }
}
