using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application.Requests.Cuentas.CrearCuenta;

public class CrearCuentaRequestHandler : IRequestHandler<CrearCuentaRequest, ApiResponseBase>
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

    public async Task<ApiResponseBase> Handle(CrearCuentaRequest request, CancellationToken cancellationToken)
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
            //string? errorMessage = _sdkCuenta.UltimoMsjError; // Not working
            _logger.LogError("Couldn't create cuenta. Error: {SdkErrorCode} - {SdkErrorMessage}", codigoError, mensajeError);
            return ApiResponseFactory.CreateFailed<CrearCuentaResponse>(request.Id,
                $"Couldn't create cuenta. Error: {codigoError} - {mensajeError}");
        }

        return ApiResponseFactory.CreateSuccessfull<CrearCuentaResponse, CrearCuentaResponseModel>(request.Id,
            new CrearCuentaResponseModel
            {
                Cuenta = await _cuentaRepository.BuscarPorIdAsync(cuentaId, request.Options, cancellationToken)
            });
    }
}
