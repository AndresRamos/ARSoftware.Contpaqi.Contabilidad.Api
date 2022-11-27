namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IDiarioRepository
{
    Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken);
}
