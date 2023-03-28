namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ITipoPolizaRepository
{
    Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken);
}
