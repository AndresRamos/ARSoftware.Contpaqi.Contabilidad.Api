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

public sealed class TipoPolizaRepository : ITipoPolizaRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public TipoPolizaRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistePorCodigoAsync(int codigo, CancellationToken cancellationToken)
    {
        return await _context.TiposPolizas.AnyAsync(m => m.Codigo.Trim() == codigo.ToString(), cancellationToken);
    }

    public async Task<IEnumerable<TipoPoliza>> BuscarPorRequestModelAsync(BuscarTiposPolizaRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        var tiposPolizaList = new List<TipoPoliza>();

        IQueryable<TiposPolizas> cuentasQuery = string.IsNullOrEmpty(requestModel.SqlQuery)
            ? _context.TiposPolizas.AsQueryable()
            : _context.TiposPolizas.FromSqlRaw($"SELECT * FROM TiposPolizas WHERE {requestModel.SqlQuery}");

        if (requestModel.Id.HasValue)
            cuentasQuery = cuentasQuery.Where(p => p.Id == requestModel.Id);

        if (requestModel.Codigo.HasValue)
            cuentasQuery = cuentasQuery.Where(p => p.Codigo.Trim() == requestModel.Codigo.ToString());

        List<TipoPolizaSql> tiposPolizaSql =
            await cuentasQuery.ProjectTo<TipoPolizaSql>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        foreach (TipoPolizaSql tipoPolizaSql in tiposPolizaSql)
        {
            var tipoPoliza = _mapper.Map<TipoPoliza>(tipoPolizaSql);

            await CargarDatosRelacionadosAsync(tipoPoliza, tipoPolizaSql, loadRelatedDataOptions, cancellationToken);

            tiposPolizaList.Add(tipoPoliza);
        }

        return tiposPolizaList;
    }

    private async Task CargarDatosRelacionadosAsync(TipoPoliza tipoPoliza, TipoPolizaSql tipoPolizaSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            tipoPoliza.DatosExtra = (await _context.TiposPolizas.FirstAsync(m => m.Id == tipoPolizaSql.Id, cancellationToken))
                .ToDatosDictionary<TiposPolizas>();
    }
}
