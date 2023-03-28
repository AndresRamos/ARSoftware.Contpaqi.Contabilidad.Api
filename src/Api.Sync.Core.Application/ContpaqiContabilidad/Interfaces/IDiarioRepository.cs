using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IDiarioRepository
{
    Task<bool> ExistePorCodgoAsync(string codigo, CancellationToken cancellationToken);
    Task<DiarioEspecial?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
