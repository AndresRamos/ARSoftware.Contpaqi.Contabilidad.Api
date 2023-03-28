using Api.Core.Domain.Models;
using FluentValidation;

namespace Api.Core.Application.Requests.Validators;

public sealed class CreateCuentaModelValidator : AbstractValidator<Cuenta>
{
    public CreateCuentaModelValidator()
    {
        RuleFor(m => m.Codigo).NotEmpty();
        RuleFor(m => m.Nombre).NotEmpty();
        RuleFor(m => m.NombreOtroIdioma).NotEmpty();
        RuleFor(m => m.CodigoCuentaAcumula).NotEmpty();
        RuleFor(m => m.Tipo).NotNull();
        RuleFor(m => m.CuentaDeMayor).NotNull();
        RuleFor(m => m.SegmentoNegocio).NotNull();
        RuleFor(m => m.Moneda).NotNull();
        RuleFor(m => m.AgrupadorSat).NotNull();
    }
}
