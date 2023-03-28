using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IPolizaRepository
{
    Task<Poliza?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
