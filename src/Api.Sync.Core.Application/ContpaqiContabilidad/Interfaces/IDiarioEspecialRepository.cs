using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IDiarioEspecialRepository
{
    Task<bool> ExistePorCodgoAsync(string codigo, CancellationToken cancellationToken);
    Task<DiarioEspecial?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<IEnumerable<DiarioEspecial>> BuscarPorRequestModelAsync(BuscarDiariosEspecialesRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
