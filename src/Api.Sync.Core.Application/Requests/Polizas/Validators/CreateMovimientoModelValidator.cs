using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Polizas.Validators;

public sealed class CreateMovimientoModelValidator : AbstractValidator<Movimiento>
{
    public CreateMovimientoModelValidator(ICuentaRepository cuentaRepository, ISegmentoNegocioRepository segmentoNegocioRepository,
        IDiarioEspecialRepository diarioEspecialRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.Numero).NotEmpty();

        RuleFor(m => m.Tipo).NotEmpty();

        RuleFor(m => m.Cuenta.Codigo)
            .NotEmpty()
            .MustAsync(cuentaRepository.ExistePorCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not a valid cuenta.");

        RuleFor(m => m.Importe).NotEmpty();

        RuleFor(m => m.Referencia).NotNull();

        RuleFor(m => m.Concepto).NotNull();

        RuleFor(m => m.SegmentoNegocio!.Codigo)
            .MustAsync(segmentoNegocioRepository.ExistePorCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not a valid segmento de negocio.")
            .When(movimiento => !string.IsNullOrWhiteSpace(movimiento.SegmentoNegocio?.Codigo));

        RuleFor(m => m.Diario!.Codigo)
            .MustAsync(diarioEspecialRepository.ExistePorCodgoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not a valid diario especial.")
            .When(movimiento => !string.IsNullOrWhiteSpace(movimiento.Diario?.Codigo));

        RuleFor(m => m.Uuid)
            .Must(s => Guid.TryParse(s, out _))
            .WithMessage("{PropertyName} {PropertyValue} is not a valid UUID")
            .When(movimiento => !string.IsNullOrWhiteSpace(movimiento.Uuid));
    }
}
