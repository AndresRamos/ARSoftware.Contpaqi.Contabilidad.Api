using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Cuentas.Validators;

public sealed class CreateCuentaModelValidator : AbstractValidator<Cuenta>
{
    public CreateCuentaModelValidator(ICuentaRepository cuentaRepository,
                                      ISegmentoNegocioRepository segmentoNegocioRepository,
                                      IMonedaRepository monedaRepository,
                                      IAgrupadorSatRepository agrupadorSatRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.Codigo)
            .NotEmpty()
            .MustAsync(async (cuenta, cancellationToken) => await cuentaRepository.ExistsByCodigoAsync(cuenta, cancellationToken) == false)
            .WithMessage("{PropertyName} {PropertyValue} already exists.");

        RuleFor(m => m.Nombre).NotEmpty();

        RuleFor(m => m.NombreOtroIdioma).NotEmpty();

        RuleFor(m => m.CodigoCuentaAcumula)
            .NotEmpty()
            .MustAsync(cuentaRepository.ExistsByCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not valid.");

        RuleFor(m => m.Tipo).NotNull();

        RuleFor(m => m.CuentaDeMayor).NotNull();

        RuleFor(m => m.SegmentoNegocio)
            .NotNull()
            .MustAsync(segmentoNegocioRepository.ExistsByCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not valid.")
            .When(cuenta => !string.IsNullOrWhiteSpace(cuenta.SegmentoNegocio), ApplyConditionTo.CurrentValidator);

        RuleFor(m => m.Moneda)
            .NotEmpty()
            .MustAsync(monedaRepository.ExistsByCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not valid.");

        RuleFor(m => m.AgrupadorSat)
            .NotNull()
            .MustAsync(agrupadorSatRepository.ExistsByCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not valid.")
            .When(cuenta => !string.IsNullOrWhiteSpace(cuenta.AgrupadorSat), ApplyConditionTo.CurrentValidator);
    }
}
