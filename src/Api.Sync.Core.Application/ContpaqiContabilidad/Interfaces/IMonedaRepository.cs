namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IMonedaRepository
{
    Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken);
}
