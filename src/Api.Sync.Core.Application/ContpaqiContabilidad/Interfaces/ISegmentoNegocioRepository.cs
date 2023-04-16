using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ISegmentoNegocioRepository
{
    Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<SegmentoNegocio?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<IEnumerable<SegmentoNegocio>> BuscarPorRequestModelAsync(BuscarSegmentosNegocioRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
