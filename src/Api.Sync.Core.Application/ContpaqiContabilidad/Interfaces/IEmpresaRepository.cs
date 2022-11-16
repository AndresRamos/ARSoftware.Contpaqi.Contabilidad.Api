using Api.Sync.Core.Application.ContpaqiContabilidad.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IEmpresaRepository
{
    Task<Empresa?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
