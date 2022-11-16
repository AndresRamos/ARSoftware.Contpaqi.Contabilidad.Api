using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Commands.TerminateSdk;

public sealed class TerminateSdkCommandHandler : IRequestHandler<TerminateSdkCommand>
{
    private readonly ILogger<TerminateSdkCommandHandler> _logger;
    private readonly ISdkSesionService _sdkSesionService;

    public TerminateSdkCommandHandler(ILogger<TerminateSdkCommandHandler> logger, ISdkSesionService sdkSesionService)
    {
        _logger = logger;
        _sdkSesionService = sdkSesionService;
    }

    public Task<Unit> Handle(TerminateSdkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Terminating SDK.");

        _sdkSesionService.TerminarConexion();
        _logger.LogInformation("Conexion Inciada: {ConexionInciada}", _sdkSesionService.ConexionInciada);

        return Unit.Task;
    }
}
