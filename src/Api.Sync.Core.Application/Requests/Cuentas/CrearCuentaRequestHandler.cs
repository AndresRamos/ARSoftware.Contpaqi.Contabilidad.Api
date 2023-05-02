using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application.Requests.Cuentas;

public class CrearCuentaRequestHandler : IRequestHandler<CrearCuentaRequest, CrearCuentaResponse>
{
    private readonly ICuentaRepository _cuentaRepository;
    private readonly ILogger<CrearCuentaRequestHandler> _logger;
    private readonly TSdkCuenta _sdkCuenta;

    public CrearCuentaRequestHandler(TSdkCuenta sdkCuenta, ICuentaRepository cuentaRepository, ILogger<CrearCuentaRequestHandler> logger)
    {
        _sdkCuenta = sdkCuenta;
        _cuentaRepository = cuentaRepository;
        _logger = logger;
    }

    public async Task<CrearCuentaResponse> Handle(CrearCuentaRequest request, CancellationToken cancellationToken)
    {
        Cuenta cuenta = request.Model.Cuenta;

        _sdkCuenta.iniciarInfo();
        _sdkCuenta.Codigo = cuenta.Codigo;
        _sdkCuenta.Nombre = cuenta.Nombre;
        _sdkCuenta.NomIdioma = cuenta.NombreOtroIdioma;
        _sdkCuenta.CodigoCuentaAcumula = cuenta.CodigoCuentaAcumula;
        _sdkCuenta.Tipo = (ECUENTATIPO)cuenta.Tipo;
        _sdkCuenta.CtaMayor = (ECUENTADEMAYOR)cuenta.CuentaDeMayor;
        _sdkCuenta.AplicaSegNeg = cuenta.SegmentoNegocioEnMovimientos ? 1 : 0;
        _sdkCuenta.SegNeg = !string.IsNullOrWhiteSpace(cuenta.SegmentoNegocio?.Codigo) ? int.Parse(cuenta.SegmentoNegocio.Codigo) : 0;
        _sdkCuenta.Moneda = int.Parse(cuenta.Moneda.Codigo);
        _sdkCuenta.DigitoAgrupador = cuenta.DigitoAgrupador;
        _sdkCuenta.AgrupadorSAT = cuenta.AgrupadorSat?.Codigo ?? string.Empty;
        _sdkCuenta.FechaAlta = DateTime.Today;
        _sdkCuenta.EsBaja = 0;
        _sdkCuenta.SistOrigen = ESISTORIGEN.ORIG_CONTPAQNG;

        var cuentaId = 0;
        _sdkCuenta.crea(ref cuentaId);
        if (cuentaId == 0)
        {
            string codigoError = _sdkCuenta.getCodigoError();
            string mensajeError = _sdkCuenta.getMensajeError();
            _logger.LogError("No se pudo crear la cuenta. Error: {SdkErrorCode} - {SdkErrorMessage}", codigoError, mensajeError);
            throw new Exception($"No se pudo crear la cuenta. Error: {codigoError} - {mensajeError}");
        }

        Cuenta cuentaCreada = await _cuentaRepository.BuscarPorIdAsync(_sdkCuenta.Id, request.Options, cancellationToken) ??
                              throw new InvalidOperationException();

        return CrearCuentaResponse.CreateInstance(cuentaCreada);
    }
}
