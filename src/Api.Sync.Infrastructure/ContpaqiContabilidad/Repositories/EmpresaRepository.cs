using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Core.Application.ContpaqiContabilidad.Models;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly ContpaqiContabilidadGeneralesDbContext _context;
    private readonly IMapper _mapper;

    public EmpresaRepository(ContpaqiContabilidadGeneralesDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Empresa?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.ListaEmpresas.Where(e => e.Nombre == name)
            .ProjectTo<Empresa>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
