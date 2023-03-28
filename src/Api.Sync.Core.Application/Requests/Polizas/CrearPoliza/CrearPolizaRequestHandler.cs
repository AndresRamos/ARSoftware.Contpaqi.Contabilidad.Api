using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application.Requests.Polizas.CrearPoliza;

public class CrearPolizaRequestHandler : IRequestHandler<CrearPolizaRequest, ApiResponseBase>
{
    private readonly ILogger<CrearPolizaRequestHandler> _logger;
    private readonly IPolizaRepository _polizaRepository;
    private readonly TSdkAsocCFDI _sdkAsocCfdi;
    private readonly TSdkMovimientoPoliza _sdkMovimientoPoliza;
    private readonly TSdkPoliza _sdkPoliza;

    public CrearPolizaRequestHandler(ILogger<CrearPolizaRequestHandler> logger,
                                     IPolizaRepository polizaRepository,
                                     TSdkAsocCFDI sdkAsocCfdi,
                                     TSdkMovimientoPoliza sdkMovimientoPoliza,
                                     TSdkPoliza sdkPoliza)
    {
        _logger = logger;
        _polizaRepository = polizaRepository;
        _sdkAsocCfdi = sdkAsocCfdi;
        _sdkMovimientoPoliza = sdkMovimientoPoliza;
        _sdkPoliza = sdkPoliza;
    }

    public async Task<ApiResponseBase> Handle(CrearPolizaRequest request, CancellationToken cancellationToken)
    {
        Guid apiRequestId = request.Id;
        CrearPolizaRequestOptions options = request.Options;
        Poliza poliza = request.Model.Poliza;

        var tipoPoliza = (ETIPOPOLIZA)int.Parse(poliza.Tipo);

        _sdkPoliza.iniciarInfo();
        _sdkPoliza.Guid = Guid.NewGuid().ToString().ToUpper();
        _sdkPoliza.Tipo = tipoPoliza;
        _sdkPoliza.Fecha = poliza.Fecha;
        _sdkPoliza.Numero = options.BuscarSiguienteNumero
            ? _sdkPoliza.getUltimoNumero(poliza.Fecha.Year, poliza.Fecha.Month, tipoPoliza)
            : poliza.Numero;
        _sdkPoliza.Clase = ECLASEPOLIZA.CLASE_AFECTAR;
        _sdkPoliza.Concepto = poliza.Concepto;
        _sdkPoliza.Diario = 0;
        _sdkPoliza.SistOrigen = ESISTORIGEN.ORIG_CONTPAQNG;

        int sdkResult = _sdkPoliza.crea();
        if (sdkResult == 0)
        {
            string codigoError = _sdkPoliza.getCodigoError();
            string mensajeError = _sdkPoliza.getMensajeError();
            _logger.LogError("Couldn't create poliza. Error: {codigoError} - {mensajeError}", codigoError, mensajeError);

            return ApiResponseFactory.CreateFailed<CrearPolizaResponse>(request.Id,
                $"Couldn't create poliza. Error: {codigoError} - {mensajeError}");
        }

        try
        {
            sdkResult = _sdkPoliza.buscaPorId(_sdkPoliza.Id);
            if (sdkResult == 0)
            {
                string codigoError = _sdkPoliza.getCodigoError();
                string mensajeError = _sdkPoliza.getMensajeError();
                throw new Exception(
                    $"Couldn't find poliza with id {_sdkPoliza.Id}. This is critical since poliza was created succesfully. Error: {codigoError} - {mensajeError}");
            }

            foreach (string uuid in poliza.Uuids)
                AsociarUuid(ETIPOASOCCFDI.TIPOASOC_POLIZA, _sdkPoliza.Guid, uuid);

            foreach (Movimiento movimiento in poliza.Movimientos.OrderBy(m => m.Numero))
            {
                _sdkMovimientoPoliza.iniciarInfo();
                _sdkMovimientoPoliza.Guid = Guid.NewGuid().ToString().ToUpper();
                _sdkMovimientoPoliza.NumMovto = movimiento.Numero;
                _sdkMovimientoPoliza.TipoMovto = (ETIPOIMPORTEMOVPOLIZA)movimiento.Tipo;
                _sdkMovimientoPoliza.CodigoCuenta = movimiento.Cuenta.Codigo;
                _sdkMovimientoPoliza.Importe = movimiento.Importe;
                _sdkMovimientoPoliza.Referencia = movimiento.Referencia;
                _sdkMovimientoPoliza.SegmentoNegocio = !string.IsNullOrWhiteSpace(movimiento.SegmentoNegocio?.Codigo)
                    ? movimiento.SegmentoNegocio.Codigo
                    : string.Empty;
                _sdkMovimientoPoliza.Concepto = movimiento.Concepto;
                _sdkMovimientoPoliza.Diario =
                    !string.IsNullOrWhiteSpace(movimiento.Diario?.Codigo) ? int.Parse(movimiento.Diario.Codigo) : 0;

                sdkResult = _sdkPoliza.creaMovimiento(_sdkMovimientoPoliza);
                if (sdkResult == 0)
                {
                    string codigoError = _sdkPoliza.getCodigoError();
                    string mensajeError = _sdkPoliza.getMensajeError();
                    string movimientoJson = JsonSerializer.Serialize(movimiento, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    throw new Exception($"Couldn't create movimiento {movimientoJson} for poliza. Error: {codigoError} - {mensajeError}");
                }

                AsociarUuid(ETIPOASOCCFDI.TIPOASOC_MOVTOPOLIZA, _sdkMovimientoPoliza.Guid, movimiento.Uuid);
            }
        }
        catch (Exception e)
        {
            DeletePoliza(_sdkPoliza.Id);
            return ApiResponseFactory.CreateFailed<CrearPolizaResponse>(request.Id, e.Message);
        }

        Poliza? polizaContabilidad = await _polizaRepository.BuscarPorIdAsync(_sdkPoliza.Id, request.Options, cancellationToken);

        return ApiResponseFactory.CreateSuccessfull<CrearPolizaResponse, CrearPolizaResponseModel>(request.Id,
            new CrearPolizaResponseModel { Poliza = polizaContabilidad });
    }

    private void AsociarUuid(ETIPOASOCCFDI tipo, string guid, string uuid)
    {
        _sdkAsocCfdi.iniciarInfo();
        _sdkAsocCfdi.GuidDocumento = guid;
        _sdkAsocCfdi.TipoAsoc = tipo;
        _sdkAsocCfdi.agregaUUID(uuid);

        int result = _sdkAsocCfdi.crea();
        if (result == 0)
        {
            string codigoError = _sdkAsocCfdi.getCodigoError();
            string mensajeError = _sdkAsocCfdi.getMensajeError();
            throw new Exception($"Couldn't associate UUID: {uuid} of type {tipo}. Error: {codigoError} - {mensajeError}");
        }
    }

    private void DeletePoliza(int polizaId)
    {
        _sdkPoliza.buscaPorId(polizaId);
        _sdkPoliza.borra();
    }
}
