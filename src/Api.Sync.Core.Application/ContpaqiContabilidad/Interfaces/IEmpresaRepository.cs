using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IEmpresaRepository
{
    Task<Empresa?> BuscarPorRfcAsync(string rfc, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<IEnumerable<Empresa>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
