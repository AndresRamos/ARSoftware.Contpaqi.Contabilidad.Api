using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ISegmentoNegocioRepository
{
    Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<SegmentoNegocio?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
