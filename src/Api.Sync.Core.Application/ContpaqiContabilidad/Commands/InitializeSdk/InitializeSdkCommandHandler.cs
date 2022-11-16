using Api.Sync.Core.Application.Common;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Commands.InitializeSdk;

public sealed class InitializeSdkCommandHandler : IRequestHandler<InitializeSdkCommand>
{
    private readonly ContabilidadConfig _contabilidadConfig;
    private readonly ILogger<InitializeSdkCommandHandler> _logger;
    private readonly ISdkSesionService _sdkSesionService;

    public InitializeSdkCommandHandler(IOptions<ContabilidadConfig> contabilidadConfigOptions, ILogger<InitializeSdkCommandHandler> logger,
                                       ISdkSesionService sdkSesionService)
    {
        _logger = logger;
        _sdkSesionService = sdkSesionService;
        _contabilidadConfig = contabilidadConfigOptions.Value;
    }

    public Task<Unit> Handle(InitializeSdkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Initializing SDK.");

        _logger.LogInformation("Conexion Inciada before IniciarConexion: {ConexionInciada}", _sdkSesionService.ConexionInciada);
        _sdkSesionService.IniciarConexion();
        _logger.LogInformation("Conexion Inciada after IniciarConexion: {ConexionInciada}", _sdkSesionService.ConexionInciada);

        _logger.LogInformation("Sesion Usuario Iniciada before IniciarSesionUsuario: {SesionUsuarioIniciada}",
            _sdkSesionService.SesionUsuarioIniciada);
        _sdkSesionService.IniciarSesionUsuario(_contabilidadConfig.Usuario, _contabilidadConfig.Contrasena);
        _logger.LogInformation("Sesion Usuario Iniciada after IniciarSesionUsuario: {SesionUsuarioIniciada}",
            _sdkSesionService.SesionUsuarioIniciada);

        return Unit.Task;
    }
}
