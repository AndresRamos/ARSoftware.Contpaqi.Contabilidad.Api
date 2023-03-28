using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IMovimientoRepository
{
    Task<IEnumerable<Movimiento>> BuscarPorPolizaIdAsync(int polizaId,
                                                         ILoadRelatedDataOptions loadRelatedDataOptions,
                                                         CancellationToken cancellationToken);
}
