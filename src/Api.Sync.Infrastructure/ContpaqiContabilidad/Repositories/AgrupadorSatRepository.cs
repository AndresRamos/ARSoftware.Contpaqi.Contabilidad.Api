using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Extensions;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Models;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using ARSoftware.Contpaqi.Contabilidad.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

    public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.AgrupadoresSAT.AnyAsync(c => c.Codigo == codigo, cancellationToken);
    }

    public async Task<AgrupadorSat?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        AgrupadorSatSql? agrupadorSatSql = await _context.AgrupadoresSAT.Where(s => s.Id == id)
            .ProjectTo<AgrupadorSatSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (agrupadorSatSql is null)
            return null;

        var segmento = _mapper.Map<AgrupadorSat>(agrupadorSatSql);

        await CargarDatosRelacionadosAsync(segmento, agrupadorSatSql, loadRelatedDataOptions, cancellationToken);

        return segmento;
    }

    private async Task CargarDatosRelacionadosAsync(AgrupadorSat agrupadorSat, AgrupadorSatSql agrupadorSatSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            agrupadorSat.DatosExtra = (await _context.AgrupadoresSAT.FirstAsync(m => m.Id == agrupadorSatSql.Id, cancellationToken))
                .ToDatosDictionary<AgrupadoresSAT>();
    }
}
