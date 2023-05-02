using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Polizas.Validators;

public sealed class CrearPolizaRequestValidator : AbstractValidator<CrearPolizaRequest>
{
    public CrearPolizaRequestValidator(ITipoPolizaRepository tipoPolizaRepository, ICuentaRepository cuentaRepository,
        ISegmentoNegocioRepository segmentoNegocioRepository, IDiarioEspecialRepository diarioEspecialRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.Model).NotNull();

        RuleFor(m => m.Model.Poliza)
            .NotEmpty()
            .SetValidator(new CreatePolizaModelValidator(tipoPolizaRepository, cuentaRepository, segmentoNegocioRepository,
                diarioEspecialRepository));
    }
}
