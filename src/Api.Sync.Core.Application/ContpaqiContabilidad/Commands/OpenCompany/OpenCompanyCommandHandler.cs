using Api.Sync.Core.Application.Common;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Commands.OpenCompany;

public sealed class OpenCompanyCommandHandler : IRequestHandler<OpenCompanyCommand>
{
    private readonly ContabilidadConfig _contabilidadConfig;
    private readonly ILogger<OpenCompanyCommandHandler> _logger;
    private readonly ISdkSesionService _sdkSesionService;

    public OpenCompanyCommandHandler(IOptions<ContabilidadConfig> contabilidadConfigOptions, ILogger<OpenCompanyCommandHandler> logger,
                                     ISdkSesionService sdkSesionService)
    {
        _logger = logger;
        _sdkSesionService = sdkSesionService;
        _contabilidadConfig = contabilidadConfigOptions.Value;
    }

    public Task<Unit> Handle(OpenCompanyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Opening company.");

        _logger.LogInformation("Empresa Abierta before AbrirEmpresa: {EmpresaAbierta}", _sdkSesionService.EmpresaAbierta);
        _sdkSesionService.AbrirEmpresa(_contabilidadConfig.Empresa);
        _logger.LogInformation("Empresa Abierta after AbrirEmpresa: {EmpresaAbierta}", _sdkSesionService.EmpresaAbierta);

        return Unit.Task;
    }
}
