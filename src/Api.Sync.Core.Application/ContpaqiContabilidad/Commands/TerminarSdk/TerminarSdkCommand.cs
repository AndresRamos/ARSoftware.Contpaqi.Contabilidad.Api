using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Commands.TerminarSdk;

public sealed record TerminarSdkCommand : IRequest;

public sealed class TerminarSdkCommandHandler : IRequestHandler<TerminarSdkCommand>
{
    private readonly ILogger<TerminarSdkCommandHandler> _logger;
    private readonly ISdkSesionService _sdkSesionService;

    public TerminarSdkCommandHandler(ILogger<TerminarSdkCommandHandler> logger, ISdkSesionService sdkSesionService)
    {
        _logger = logger;
        _sdkSesionService = sdkSesionService;
    }

    public Task Handle(TerminarSdkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Terminating SDK.");

        _logger.LogInformation("Conexion Inciada before TerminarConexion: {ConexionInciada}", _sdkSesionService.ConexionInciada);
        _sdkSesionService.TerminarConexion();
        _logger.LogInformation("Conexion Inciada after TerminarConexion: {ConexionInciada}", _sdkSesionService.ConexionInciada);

        return Task.CompletedTask;
    }
}
