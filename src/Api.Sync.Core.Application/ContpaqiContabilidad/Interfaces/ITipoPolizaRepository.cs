using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ITipoPolizaRepository
{
    Task<bool> ExistePorCodigoAsync(int codigo, CancellationToken cancellationToken);

    Task<IEnumerable<TipoPoliza>> BuscarPorRequestModelAsync(BuscarTiposPolizaRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
