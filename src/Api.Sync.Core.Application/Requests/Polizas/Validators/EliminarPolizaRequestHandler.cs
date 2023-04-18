using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application.Requests.Polizas.Validators;

public sealed class EliminarPolizaRequestHandler : IRequestHandler<EliminarPolizaRequest, ApiResponse>
{
    private readonly ILogger<EliminarPolizaRequestHandler> _logger;
    private readonly IPolizaRepository _polizaRepository;
    private readonly TSdkPoliza _sdkPoliza;

    public EliminarPolizaRequestHandler(IPolizaRepository polizaRepository, TSdkPoliza sdkPoliza,
        ILogger<EliminarPolizaRequestHandler> logger)
    {
        _polizaRepository = polizaRepository;
        _sdkPoliza = sdkPoliza;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(EliminarPolizaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Poliza poliza = (await _polizaRepository.BuscarPorLlaveAsync(request.Model.LlavePoliza, request.Options, cancellationToken))!;

            _sdkPoliza.iniciarInfo();
            _sdkPoliza.buscaPorId(poliza.Id);
            int sdkResult = _sdkPoliza.borra();
            if (sdkResult == 0)
            {
                string codigoError = _sdkPoliza.getCodigoError();
                string mensajeError = _sdkPoliza.getMensajeError();
                _logger.LogError("No se pudo eliminar la poliza. Error: {codigoError} - {mensajeError}", codigoError, mensajeError);
                throw new Exception($"No se pude eliminar la poliza. Error: {codigoError} - {mensajeError}");
            }

            return ApiResponse.CreateSuccessfull(new EliminarPolizaResponse());
        }
        catch (Exception e)
        {
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
