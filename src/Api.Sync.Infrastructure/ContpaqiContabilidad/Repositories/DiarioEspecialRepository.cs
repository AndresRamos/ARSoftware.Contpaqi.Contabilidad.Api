using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Extensions;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Models;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using ARSoftware.Contpaqi.Contabilidad.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;

public sealed class DiarioEspecialRepository : IDiarioEspecialRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public DiarioEspecialRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistePorCodgoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.DiariosEspeciales.AnyAsync(d => d.Codigo.Trim() == codigo.Trim(), cancellationToken);
    }

    public async Task<DiarioEspecial?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions,
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

    public async Task<IEnumerable<DiarioEspecial>> BuscarPorRequestModelAsync(BuscarDiariosEspecialesRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        var diariosEspecialesList = new List<DiarioEspecial>();

        IQueryable<DiariosEspeciales> diariosQuery = string.IsNullOrEmpty(requestModel.SqlQuery)
            ? _context.DiariosEspeciales.AsQueryable()
            : _context.DiariosEspeciales.FromSqlRaw($"SELECT * FROM DiariosEspeciales WHERE {requestModel.SqlQuery}");

        if (requestModel.Id.HasValue)
            diariosQuery = diariosQuery.Where(p => p.Id == requestModel.Id);

        if (!string.IsNullOrWhiteSpace(requestModel.Codigo))
            diariosQuery = diariosQuery.Where(p => p.Codigo == requestModel.Codigo);

        List<DiarioEspecialSql> diariosEspecialesSql =
            await diariosQuery.ProjectTo<DiarioEspecialSql>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        foreach (DiarioEspecialSql diarioEspecialSql in diariosEspecialesSql)
        {
            var diarioEspecial = _mapper.Map<DiarioEspecial>(diarioEspecialSql);

            await CargarDatosRelacionadosAsync(diarioEspecial, diarioEspecialSql, loadRelatedDataOptions, cancellationToken);

            diariosEspecialesList.Add(diarioEspecial);
        }

        return diariosEspecialesList;
    }

    private async Task CargarDatosRelacionadosAsync(DiarioEspecial diarioEspecial, DiarioEspecialSql diarioEspecialSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            diarioEspecial.DatosExtra = (await _context.DiariosEspeciales.FirstAsync(m => m.Id == diarioEspecialSql.Id, cancellationToken))
                .ToDatosDictionary<DiariosEspeciales>();
    }
}
