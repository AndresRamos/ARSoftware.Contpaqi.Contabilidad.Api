namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IAgrupadorSatRepository
{
    Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken);
}
