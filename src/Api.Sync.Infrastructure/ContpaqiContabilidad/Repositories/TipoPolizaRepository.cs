using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;

public sealed class TipoPolizaRepository : ITipoPolizaRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public TipoPolizaRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.TiposPolizas.AnyAsync(m => m.Codigo.Trim() == codigo.Trim(), cancellationToken);
    }
}
