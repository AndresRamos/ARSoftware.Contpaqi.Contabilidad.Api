using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;

public sealed class AgrupadorSatRepository : IAgrupadorSatRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public AgrupadorSatRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistsByCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.AgrupadoresSAT.AnyAsync(c => c.Codigo == codigo, cancellationToken);
    }
}
