namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ISegmentoNegocioRepository
{
    Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken);
}
