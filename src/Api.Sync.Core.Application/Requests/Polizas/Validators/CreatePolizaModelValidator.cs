using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Polizas.Validators;

public sealed class CreatePolizaModelValidator : AbstractValidator<Poliza>
{
    public CreatePolizaModelValidator(ITipoPolizaRepository tipoPolizaRepository,
                                      ICuentaRepository cuentaRepository,
                                      ISegmentoNegocioRepository segmentoNegocioRepository,
                                      IDiarioRepository diarioRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.Tipo)
            .NotEmpty()
            .MustAsync(tipoPolizaRepository.ExistePorCodigoAsync)
            .WithMessage("{PropertyName} {PropertyValue} is not a valid tipo de poliza.");

        RuleFor(m => m.Fecha).NotEmpty();

        RuleFor(m => m.Concepto).NotEmpty();

        RuleFor(m => m.Movimientos).NotEmpty().Must(m => m.Count > 0).WithMessage("Poliza should have at least one movimiento.");

        RuleForEach(m => m.Movimientos)
            .SetValidator(new CreateMovimientoModelValidator(cuentaRepository, segmentoNegocioRepository, diarioRepository));

        RuleForEach(m => m.Uuids)
            .NotNull()
            .Must(s => string.IsNullOrWhiteSpace(s) || Guid.TryParse(s, out _))
            .WithMessage("{PropertyName} {PropertyValue} is not a valid UUID");
    }
}
