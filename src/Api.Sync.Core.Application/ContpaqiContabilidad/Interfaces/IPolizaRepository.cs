using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IPolizaRepository
{
    Task<Poliza?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<Poliza?> BuscarPorLlaveAsync(LlavePoliza llave, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken);

    Task<IEnumerable<Poliza>> BuscarPorRequestModelAsync(BuscarPolizasRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
