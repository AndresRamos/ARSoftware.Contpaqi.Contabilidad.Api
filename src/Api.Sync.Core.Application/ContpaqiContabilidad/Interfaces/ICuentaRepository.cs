using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ICuentaRepository
{
    Task<Cuenta?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
    Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken);

    Task<IEnumerable<Cuenta>> BuscarPorRequestModelAsync(BuscarCuentasRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
