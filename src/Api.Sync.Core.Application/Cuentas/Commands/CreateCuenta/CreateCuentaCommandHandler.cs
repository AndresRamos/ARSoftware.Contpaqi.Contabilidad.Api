using Api.SharedKernel.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application.Cuentas.Commands.CreateCuenta;

public class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, CreateCuentaResponse>
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly TSdkCuenta _sdkCuenta;

    public CreateCuentaCommandHandler(TSdkCuenta sdkCuenta, ICuentaRepository cuentaRepository)
    {
        _sdkCuenta = sdkCuenta;
        _cuentaRepository = cuentaRepository;
    }

    public async Task<CreateCuentaResponse> Handle(CreateCuentaCommand request, CancellationToken cancellationToken)
    {
        Cuenta cuenta = request.ApiRequest.Model;

        _sdkCuenta.iniciarInfo();
        _sdkCuenta.Codigo = cuenta.Codigo;
        _sdkCuenta.Nombre = cuenta.Nombre;
        _sdkCuenta.NomIdioma = cuenta.NombreOtroIdioma;
        //_sdkCuenta.CodigoCuentaAcumula = cuenta.CodigoCuentaAcumula;
        _sdkCuenta.Tipo = (ECUENTATIPO)cuenta.Tipo;
        _sdkCuenta.CtaMayor = (ECUENTADEMAYOR)cuenta.CuentaDeMayor;
        _sdkCuenta.AplicaSegNeg = cuenta.SegmentoNegocioEnMovimientos ? 1 : 0;
        _sdkCuenta.SegNeg = !string.IsNullOrEmpty(cuenta.SegmentoNegocio) ? int.Parse(cuenta.SegmentoNegocio) : 0;
        _sdkCuenta.Moneda = int.Parse(cuenta.Moneda);
        _sdkCuenta.DigitoAgrupador = cuenta.DigitoAgrupador;
        _sdkCuenta.AgrupadorSAT = cuenta.AgrupadorSat;
        _sdkCuenta.FechaAlta = DateTime.Today;
        _sdkCuenta.EsBaja = 0;
        _sdkCuenta.SistOrigen = ESISTORIGEN.ORIG_CONTPAQNG;

        var cuentaId = 0;
        _sdkCuenta.crea(ref cuentaId);

        if (cuentaId == 0)
        {
            string codigoError = _sdkCuenta.getCodigoError();
            string mensajeError = _sdkCuenta.getMensajeError();
            string? errorMessage = _sdkCuenta.UltimoMsjError;
            return CreateCuentaResponse.CreateFailed(mensajeError);
        }

        Cuenta? cuentaContabilidad = await _cuentaRepository.GetByIdAsync(cuentaId, cancellationToken);

        return CreateCuentaResponse.CreateSuccess(cuentaContabilidad!);
    }
}
