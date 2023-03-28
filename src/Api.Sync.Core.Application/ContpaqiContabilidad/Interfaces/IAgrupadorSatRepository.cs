using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IAgrupadorSatRepository
{
    Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<AgrupadorSat?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
