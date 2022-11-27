using System.Text.Json;
using Api.SharedKernel.Models;
using Api.SharedKernel.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application.Polizas.Commands.CreatePoliza;

public class CreatePolizaCommandHandler : IRequestHandler<CreatePolizaCommand, CreatePolizaResponse>
{
    private readonly ILogger<CreatePolizaCommandHandler> _logger;
    private readonly IPolizaRepository _polizaRepository;
    private readonly TSdkAsocCFDI _sdkAsocCfdi;
    private readonly TSdkMovimientoPoliza _sdkMovimientoPoliza;
    private readonly TSdkPoliza _sdkPoliza;

    public CreatePolizaCommandHandler(ILogger<CreatePolizaCommandHandler> logger, IPolizaRepository polizaRepository,
                                      TSdkAsocCFDI sdkAsocCfdi, TSdkMovimientoPoliza sdkMovimientoPoliza, TSdkPoliza sdkPoliza)
    {
        _logger = logger;
        _polizaRepository = polizaRepository;
        _sdkAsocCfdi = sdkAsocCfdi;
        _sdkMovimientoPoliza = sdkMovimientoPoliza;
        _sdkPoliza = sdkPoliza;
    }

    public async Task<CreatePolizaResponse> Handle(CreatePolizaCommand request, CancellationToken cancellationToken)
    {
        Guid apiRequestId = request.ApiRequest.Id;
        CreatePolizaOptions options = request.ApiRequest.Options;
        Poliza poliza = request.ApiRequest.Model;

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
            return CreatePolizaResponse.CreateFailed($"Couldn't create poliza. Error: {codigoError} - {mensajeError}");
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
                _sdkMovimientoPoliza.CodigoCuenta = movimiento.Cuenta;
                _sdkMovimientoPoliza.Importe = movimiento.Importe;
                _sdkMovimientoPoliza.Referencia = movimiento.Referencia;
                _sdkMovimientoPoliza.SegmentoNegocio =
                    !string.IsNullOrWhiteSpace(movimiento.SegmentoNegocio) ? movimiento.SegmentoNegocio : string.Empty;
                _sdkMovimientoPoliza.Concepto = movimiento.Concepto;
                _sdkMovimientoPoliza.Diario = !string.IsNullOrWhiteSpace(movimiento.Diario) ? int.Parse(movimiento.Diario) : 0;

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
            return CreatePolizaResponse.CreateFailed(e.Message);
        }

        Poliza? polizaContabilidad = await _polizaRepository.GetByIdAsync(_sdkPoliza.Id, cancellationToken);

        return CreatePolizaResponse.CreateSuccess(polizaContabilidad);
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
