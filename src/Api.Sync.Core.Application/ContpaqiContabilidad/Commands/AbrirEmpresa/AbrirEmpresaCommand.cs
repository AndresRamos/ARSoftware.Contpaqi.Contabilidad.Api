﻿using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Commands.AbrirEmpresa;

public sealed record AbrirEmpresaCommand : IRequest;

public sealed class AbrirEmpresaCommandHandler : IRequestHandler<AbrirEmpresaCommand>
{
    private readonly ContpaqiContabilidadConfig _contpaqiContabilidadConfig;
    private readonly ILogger<AbrirEmpresaCommandHandler> _logger;
    private readonly ISdkSesionService _sdkSesionService;

    public AbrirEmpresaCommandHandler(IOptions<ContpaqiContabilidadConfig> contabilidadConfigOptions,
        ILogger<AbrirEmpresaCommandHandler> logger, ISdkSesionService sdkSesionService)
    {
        _logger = logger;
        _sdkSesionService = sdkSesionService;
        _contpaqiContabilidadConfig = contabilidadConfigOptions.Value;
    }

    public Task Handle(AbrirEmpresaCommand request, CancellationToken cancellationToken)
    {
        // Cerrar empresa si ya está abierta y no es la misma que se quiere abrir
        if (_sdkSesionService.EmpresaAbierta && _contpaqiContabilidadConfig.Empresa.BaseDatos != _sdkSesionService.BaseDatos)
            _sdkSesionService.CierraEmpresa();

        _logger.LogInformation("Empresa Abierta before AbrirEmpresa: {EmpresaAbierta}", _sdkSesionService.EmpresaAbierta);
        _sdkSesionService.AbrirEmpresa(_contpaqiContabilidadConfig.Empresa.BaseDatos);
        _logger.LogInformation("Empresa Abierta after AbrirEmpresa: {EmpresaAbierta}", _sdkSesionService.EmpresaAbierta);

        return Task.CompletedTask;
    }
}
