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

public sealed class SegmentoNegocioRepository : ISegmentoNegocioRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public SegmentoNegocioRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.SegmentosNegocio.AnyAsync(m => m.Codigo.Trim() == codigo.Trim(), cancellationToken);
    }

    public async Task<SegmentoNegocio?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        SegmentoNegocioSql? segmentoSql = await _context.SegmentosNegocio.Where(s => s.Id == id)
            .ProjectTo<SegmentoNegocioSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (segmentoSql is null)
            return null;

        var segmento = _mapper.Map<SegmentoNegocio>(segmentoSql);

        await CargarDatosRelacionadosAsync(segmento, segmentoSql, loadRelatedDataOptions, cancellationToken);

        return segmento;
    }

    public async Task<IEnumerable<SegmentoNegocio>> BuscarPorRequestModelAsync(BuscarSegmentosNegocioRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        var segmentosNegocioList = new List<SegmentoNegocio>();

        IQueryable<SegmentosNegocio> segmentosNegocioQuery = string.IsNullOrEmpty(requestModel.SqlQuery)
            ? _context.SegmentosNegocio.AsQueryable()
            : _context.SegmentosNegocio.FromSqlRaw($"SELECT * FROM SegmentosNegocio WHERE {requestModel.SqlQuery}");

        if (requestModel.Id.HasValue)
            segmentosNegocioQuery = segmentosNegocioQuery.Where(p => p.Id == requestModel.Id);

        if (!string.IsNullOrWhiteSpace(requestModel.Codigo))
            segmentosNegocioQuery = segmentosNegocioQuery.Where(p => p.Codigo == requestModel.Codigo);

        List<SegmentoNegocioSql> segmentosNegocioSql = await segmentosNegocioQuery
            .ProjectTo<SegmentoNegocioSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (SegmentoNegocioSql segmentoNegocioSql in segmentosNegocioSql)
        {
            var segmentoNegocio = _mapper.Map<SegmentoNegocio>(segmentoNegocioSql);

            await CargarDatosRelacionadosAsync(segmentoNegocio, segmentoNegocioSql, loadRelatedDataOptions, cancellationToken);

            segmentosNegocioList.Add(segmentoNegocio);
        }

        return segmentosNegocioList;
    }

    private async Task CargarDatosRelacionadosAsync(SegmentoNegocio segmentoNegocio, SegmentoNegocioSql segmentoNegocioSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            segmentoNegocio.DatosExtra = (await _context.SegmentosNegocio.FirstAsync(m => m.Id == segmentoNegocioSql.Id, cancellationToken))
                .ToDatosDictionary<SegmentosNegocio>();
    }
}
