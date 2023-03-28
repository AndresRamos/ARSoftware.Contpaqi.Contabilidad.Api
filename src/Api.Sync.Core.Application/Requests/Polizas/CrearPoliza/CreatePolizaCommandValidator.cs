using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Core.Application.Requests.Polizas.Validators;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Polizas.CrearPoliza;

public sealed class CreatePolizaCommandValidator : AbstractValidator<CrearPolizaRequest>
{
    public CreatePolizaCommandValidator(ITipoPolizaRepository tipoPolizaRepository,
                                        ICuentaRepository cuentaRepository,
                                        ISegmentoNegocioRepository segmentoNegocioRepository,
                                        IDiarioRepository diarioRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.Model).NotNull();

        RuleFor(m => m.Model.Poliza)
            .NotEmpty()
            .SetValidator(new CreatePolizaModelValidator(tipoPolizaRepository,
                cuentaRepository,
                segmentoNegocioRepository,
                diarioRepository));
    }
}
