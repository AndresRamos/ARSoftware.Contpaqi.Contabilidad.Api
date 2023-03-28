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

public sealed class DiarioRepository : IDiarioRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public DiarioRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistePorCodgoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.DiariosEspeciales.AnyAsync(d => d.Codigo.Trim() == codigo.Trim(), cancellationToken);
    }

    public async Task<DiarioEspecial?> BuscarPorIdAsync(int id,
                                                        ILoadRelatedDataOptions loadRelatedDataOptions,
                                                        CancellationToken cancellationToken)
    {
        DiarioEspecialSql? diarioSql = await _context.DiariosEspeciales.Where(s => s.Id == id)
            .ProjectTo<DiarioEspecialSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (diarioSql is null)
            return null;

        var diario = _mapper.Map<DiarioEspecial>(diarioSql);

        await CargarDatosRelacionadosAsync(diario, diarioSql, loadRelatedDataOptions, cancellationToken);

        return diario;
    }

    private async Task CargarDatosRelacionadosAsync(DiarioEspecial diarioEspecial,
                                                    DiarioEspecialSql diarioEspecialSql,
                                                    ILoadRelatedDataOptions loadRelatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            diarioEspecial.DatosExtra = (await _context.DiariosEspeciales.FirstAsync(m => m.Id == diarioEspecialSql.Id, cancellationToken))
                .ToDatosDictionary<DiariosEspeciales>();
    }
}
