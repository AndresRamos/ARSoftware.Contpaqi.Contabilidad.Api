using Api.SharedKernel.Models;
using Api.SharedKernel.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application.Cuentas.Commands.CreateCuenta;

public class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, CreateCuentaResponse>
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly ILogger<CreateCuentaCommandHandler> _logger;
    private readonly TSdkCuenta _sdkCuenta;

    public CreateCuentaCommandHandler(TSdkCuenta sdkCuenta, ICuentaRepository cuentaRepository, ILogger<CreateCuentaCommandHandler> logger)
    {
        _sdkCuenta = sdkCuenta;
        _cuentaRepository = cuentaRepository;
        _logger = logger;
    }

    public async Task<CreateCuentaResponse> Handle(CreateCuentaCommand request, CancellationToken cancellationToken)
    {
        Cuenta cuenta = request.ApiRequest.Model;

        _sdkCuenta.iniciarInfo();
        _sdkCuenta.Codigo = cuenta.Codigo;
        _sdkCuenta.Nombre = cuenta.Nombre;
        _sdkCuenta.NomIdioma = cuenta.NombreOtroIdioma;
        _sdkCuenta.CodigoCuentaAcumula = cuenta.CodigoCuentaAcumula;
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
            //string? errorMessage = _sdkCuenta.UltimoMsjError; // Not working
            _logger.LogError("Couldn't create cuenta. Error: {SdkErrorCode} - {SdkErrorMessage}", codigoError, mensajeError);
            return CreateCuentaResponse.CreateFailed($"Couldn't create cuenta. Error: {codigoError} - {mensajeError}");
        }

        Cuenta? cuentaContabilidad = await _cuentaRepository.GetByIdAsync(cuentaId, cancellationToken);

        return CreateCuentaResponse.CreateSuccess(cuentaContabilidad!);
    }
}
