namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ITipoPolizaRepository
{
    Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken);
}
