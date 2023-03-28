using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Core.Application.Requests.Cuentas.Validators;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Cuentas.CrearCuenta;

public class CreateCuentaCommandValidator : AbstractValidator<CrearCuentaRequest>
{
    public CreateCuentaCommandValidator(ICuentaRepository cuentaRepository,
                                        ISegmentoNegocioRepository segmentoNegocioRepository,
                                        IMonedaRepository monedaRepository,
                                        IAgrupadorSatRepository agrupadorSatRepository)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.Model).NotNull();

        RuleFor(m => m.Model.Cuenta)
            .SetValidator(new CreateCuentaModelValidator(cuentaRepository,
                segmentoNegocioRepository,
                monedaRepository,
                agrupadorSatRepository));
    }
}
