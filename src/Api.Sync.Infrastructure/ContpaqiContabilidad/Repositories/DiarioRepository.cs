using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;

public sealed class DiarioRepository : IDiarioRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public DiarioRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.DiariosEspeciales.AnyAsync(d => d.Codigo.Trim() == codigo.Trim(), cancellationToken);
    }
}
