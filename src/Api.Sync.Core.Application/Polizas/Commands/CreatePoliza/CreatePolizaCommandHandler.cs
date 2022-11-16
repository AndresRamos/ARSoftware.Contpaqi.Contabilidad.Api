using Api.SharedKernel.Models;
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
        _logger.LogInformation("{@Request}", request.Request);

        Guid apiRequestId = request.Request.Id;
        CreatePolizaOptions options = request.Request.Options;
        Poliza poliza = request.Request.Model;

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
            throw new Exception(
                $"Couldn't create poliza for request {apiRequestId}. Error: {_sdkPoliza.getCodigoError()} {_sdkPoliza.getMensajeError()} {_sdkPoliza.UltimoMsjError}");
        }

        sdkResult = _sdkPoliza.buscaPorId(_sdkPoliza.Id);
        if (sdkResult == 0)
            throw new Exception($"Couldn't find poliza with ID {_sdkPoliza.Id}. Error: {_sdkPoliza.UltimoMsjError}");

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
            _sdkMovimientoPoliza.SegmentoNegocio = movimiento.SegmentoNegocio;
            _sdkMovimientoPoliza.Concepto = movimiento.Concepto;
            _sdkMovimientoPoliza.Diario = int.Parse(movimiento.Diario);

            sdkResult = _sdkPoliza.creaMovimiento(_sdkMovimientoPoliza);
            if (sdkResult == 0)
            {
                string? mensajeError = _sdkPoliza.UltimoMsjError;
                throw new Exception($"Couldn't create movimiento for poliza. Error: {mensajeError}.");
            }

            AsociarUuid(ETIPOASOCCFDI.TIPOASOC_MOVTOPOLIZA, _sdkMovimientoPoliza.Guid, movimiento.Uuid);
        }

        Poliza? polizaContabilidad = await _polizaRepository.GetByIdAsync(_sdkPoliza.Id, cancellationToken);

        return new CreatePolizaResponse
        {
            Id = request.Request.Id,
            Model = polizaContabilidad,
            IsSuccess = true
        };
    }

    private void AsociarUuid(ETIPOASOCCFDI tipo, string guid, string uuid)
    {
        _sdkAsocCfdi.iniciarInfo();
        _sdkAsocCfdi.GuidDocumento = guid;
        _sdkAsocCfdi.TipoAsoc = tipo;
        _sdkAsocCfdi.agregaUUID(uuid);

        int result = _sdkAsocCfdi.crea();
        if (result == 0)
            throw new Exception($"Couldn't associate UUID: {uuid}. Error: {_sdkAsocCfdi.getMensajeError()}");
    }
}
