using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Core.Application.Cuentas.Validators;
using FluentValidation;

namespace Api.Sync.Core.Application.Cuentas.Commands.CreateCuenta;

public class CreateCuentaCommandValidator : AbstractValidator<CreateCuentaCommand>
{
    public CreateCuentaCommandValidator(ICuentaRepository cuentaRepository, ISegmentoNegocioRepository segmentoNegocioRepository,
                                        IMonedaRepository monedaRepository, IAgrupadorSatRepository agrupadorSatRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(m => m.ApiRequest).NotNull();

        RuleFor(m => m.ApiRequest.Model).NotNull();

        RuleFor(m => m.ApiRequest.Model)
            .SetValidator(new CreateCuentaModelValidator(cuentaRepository, segmentoNegocioRepository, monedaRepository,
                agrupadorSatRepository));
    }
}
