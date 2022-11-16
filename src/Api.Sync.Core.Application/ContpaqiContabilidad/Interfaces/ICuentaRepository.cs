using Api.SharedKernel.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface ICuentaRepository
{
    Task<Cuenta?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
