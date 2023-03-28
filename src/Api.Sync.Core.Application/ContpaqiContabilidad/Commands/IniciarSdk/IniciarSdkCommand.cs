using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Commands.IniciarSdk;

public sealed record IniciarSdkCommand : IRequest;

public sealed class IniciarSdkCommandHandler : IRequestHandler<IniciarSdkCommand>
{
    private readonly ContpaqiContabilidadConfig _contpaqiContabilidadConfig;
    private readonly ILogger<IniciarSdkCommandHandler> _logger;
    private readonly ISdkSesionService _sdkSesionService;

    public IniciarSdkCommandHandler(IOptions<ContpaqiContabilidadConfig> contabilidadConfigOptions,
                                    ILogger<IniciarSdkCommandHandler> logger,
                                    ISdkSesionService sdkSesionService)
    {
        _logger = logger;
        _sdkSesionService = sdkSesionService;
        _contpaqiContabilidadConfig = contabilidadConfigOptions.Value;
    }

    public Task Handle(IniciarSdkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Initializing SDK.");

        _logger.LogInformation("Conexion Inciada before IniciarConexion: {ConexionInciada}", _sdkSesionService.ConexionInciada);
        _sdkSesionService.IniciarConexion();
        _logger.LogInformation("Conexion Inciada after IniciarConexion: {ConexionInciada}", _sdkSesionService.ConexionInciada);

        _logger.LogInformation("Sesion Usuario Iniciada before IniciarSesionUsuario: {SesionUsuarioIniciada}",
            _sdkSesionService.SesionUsuarioIniciada);
        _sdkSesionService.IniciarSesionUsuario(_contpaqiContabilidadConfig.Usuario, _contpaqiContabilidadConfig.Contrasena);
        _logger.LogInformation("Sesion Usuario Iniciada after IniciarSesionUsuario: {SesionUsuarioIniciada}",
            _sdkSesionService.SesionUsuarioIniciada);

        return Task.CompletedTask;
    }
}
