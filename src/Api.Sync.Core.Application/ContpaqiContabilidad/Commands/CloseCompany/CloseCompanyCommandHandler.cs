using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Commands.CloseCompany;

public sealed class CloseCompanyCommandHandler : IRequestHandler<CloseCompanyCommand>
{
    private readonly ILogger<CloseCompanyCommandHandler> _logger;
    private readonly ISdkSesionService _sdkSesionService;

    public CloseCompanyCommandHandler(ILogger<CloseCompanyCommandHandler> logger, ISdkSesionService sdkSesionService)
    {
        _logger = logger;
        _sdkSesionService = sdkSesionService;
    }

    public Task<Unit> Handle(CloseCompanyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Closing company.");

        _logger.LogInformation("Empresa Abierta before CierraEmpresa: {EmpresaAbierta}", _sdkSesionService.EmpresaAbierta);
        _sdkSesionService.CierraEmpresa();
        _logger.LogInformation("Empresa Abierta after CierraEmpresa: {EmpresaAbierta}", _sdkSesionService.EmpresaAbierta);

        return Unit.Task;
    }
}
