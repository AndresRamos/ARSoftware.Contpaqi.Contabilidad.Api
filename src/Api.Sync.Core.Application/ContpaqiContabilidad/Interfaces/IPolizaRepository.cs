﻿using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;

public interface IPolizaRepository
{
    Task<Poliza?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
