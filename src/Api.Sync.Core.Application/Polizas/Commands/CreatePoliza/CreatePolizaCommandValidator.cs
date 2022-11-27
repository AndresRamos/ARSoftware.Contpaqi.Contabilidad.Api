using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Core.Application.Polizas.Models;
using FluentValidation;

namespace Api.Sync.Core.Application.Polizas.Commands.CreatePoliza;

public sealed class CreatePolizaCommandValidator : AbstractValidator<CreatePolizaCommand>
{
    public CreatePolizaCommandValidator(ITipoPolizaRepository tipoPolizaRepository, ICuentaRepository cuentaRepository,
                                        ISegmentoNegocioRepository segmentoNegocioRepository, IDiarioRepository diarioRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.ApiRequest).NotNull();

        RuleFor(m => m.ApiRequest.Model).NotNull();

        RuleFor(m => m.ApiRequest.Model)
            .NotEmpty()
            .SetValidator(new CreatePolizaModelValidator(tipoPolizaRepository, cuentaRepository, segmentoNegocioRepository,
                diarioRepository));
    }
}
