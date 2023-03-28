using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Commands.CerrarEmpresa;

public sealed record CerrarEmpresaCommand : IRequest;

public sealed class CerrarEmpresaCommandHandler : IRequestHandler<CerrarEmpresaCommand>
{
    private readonly ILogger<CerrarEmpresaCommandHandler> _logger;
    private readonly ISdkSesionService _sdkSesionService;

    public CerrarEmpresaCommandHandler(ILogger<CerrarEmpresaCommandHandler> logger, ISdkSesionService sdkSesionService)
    {
        _logger = logger;
        _sdkSesionService = sdkSesionService;
    }

    public Task Handle(CerrarEmpresaCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Closing company.");

        _logger.LogInformation("Empresa Abierta before CierraEmpresa: {EmpresaAbierta}", _sdkSesionService.EmpresaAbierta);
        _sdkSesionService.CierraEmpresa();
        _logger.LogInformation("Empresa Abierta after CierraEmpresa: {EmpresaAbierta}", _sdkSesionService.EmpresaAbierta);

        return Task.CompletedTask;
    }
}
