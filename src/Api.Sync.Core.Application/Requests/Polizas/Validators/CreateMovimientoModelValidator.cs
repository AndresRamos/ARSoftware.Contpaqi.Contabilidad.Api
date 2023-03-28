using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Polizas.Validators;

public sealed class CreateMovimientoModelValidator : AbstractValidator<Movimiento>
{
    public CreateMovimientoModelValidator(ICuentaRepository cuentaRepository,
                                          ISegmentoNegocioRepository segmentoNegocioRepository,
                                          IDiarioRepository diarioRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.Numero).NotEmpty();

        RuleFor(m => m.Tipo).NotEmpty();

        RuleFor(m => m.Cuenta)
            .NotEmpty()
            .MustAsync(cuentaRepository.ExistsByCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not a valid cuenta.");

        RuleFor(m => m.Importe).NotEmpty();

        RuleFor(m => m.Referencia).NotNull();

        RuleFor(m => m.Concepto).NotNull();

        RuleFor(m => m.SegmentoNegocio)
            .NotNull()
            .MustAsync(segmentoNegocioRepository.ExistsByCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not a valid segmento de negocio.")
            .When(movimiento => !string.IsNullOrWhiteSpace(movimiento.SegmentoNegocio), ApplyConditionTo.CurrentValidator);

        RuleFor(m => m.Diario)
            .NotNull()
            .MustAsync(diarioRepository.ExistsByCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not a valid diario especial.")
            .When(movimiento => !string.IsNullOrWhiteSpace(movimiento.Diario), ApplyConditionTo.CurrentValidator);

        RuleFor(m => m.Uuid)
            .NotNull()
            .Must(s => Guid.TryParse(s, out _))
            .WithMessage("{PropertyName} {PropertyValue} is not a valid UUID")
            .When(movimiento => !string.IsNullOrWhiteSpace(movimiento.Uuid), ApplyConditionTo.CurrentValidator);
    }
}
