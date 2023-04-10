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

public sealed class PolizaRepository : IPolizaRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMovimientoRepository _movimientoRepository;

    public PolizaRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _movimientoRepository = new MovimientoRepository(context, mapper);
    }

    public async Task<Poliza?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        PolizaSql? polizaSql = await _context.Polizas.Where(p => p.Id == id)
            .ProjectTo<PolizaSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (polizaSql is null)
            return null;

        var poliza = _mapper.Map<Poliza>(polizaSql);

        await CargarDatosRelacionadosAsync(poliza, polizaSql, loadRelatedDataOptions, cancellationToken);

        return poliza;
    }

    public async Task<Poliza?> BuscarPorLlaveAsync(LlavePoliza llave,
                                                   ILoadRelatedDataOptions loadRelatedDataOptions,
                                                   CancellationToken cancellationToken)
    {
        PolizaSql? polizaSql = await _context.Polizas
            .Where(p => p.TipoPol == llave.Tipo && p.Ejercicio == llave.Ejercicio && p.Periodo == llave.Periodo && p.Folio == llave.Numero)
            .ProjectTo<PolizaSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (polizaSql is null)
            return null;

        var poliza = _mapper.Map<Poliza>(polizaSql);

        await CargarDatosRelacionadosAsync(poliza, polizaSql, loadRelatedDataOptions, cancellationToken);

        return poliza;
    }

    public async Task<IEnumerable<Poliza>> BuscarPorRequestModelAsync(BuscarPolizasRequestModel requestModel,
                                                                      ILoadRelatedDataOptions loadRelatedDataOptions,
                                                                      CancellationToken cancellationToken)
    {
        var polizasList = new List<Poliza>();

        IQueryable<Polizas> polizasQuery = string.IsNullOrEmpty(requestModel.SqlQuery)
            ? _context.Polizas.AsQueryable()
            : _context.Polizas.FromSqlRaw($"SELECT * FROM Polizas WHERE {requestModel.SqlQuery}");

        if (requestModel.Id.HasValue)
            polizasQuery = polizasQuery.Where(p => p.Id == requestModel.Id);

        if (requestModel.FechaInicio.HasValue)
        {
            var fechaInicio = requestModel.FechaInicio.Value.ToDateTime(TimeOnly.MinValue);
            polizasQuery = polizasQuery.Where(p => p.Fecha >= fechaInicio);
        }

        if (requestModel.FechaFin.HasValue)
        {
            var fechaFin = requestModel.FechaFin.Value.ToDateTime(TimeOnly.MaxValue);
            polizasQuery = polizasQuery.Where(p => p.Fecha <= fechaFin);
        }

        if (requestModel.Tipo.HasValue)
            polizasQuery = polizasQuery.Where(p => p.TipoPol == requestModel.Tipo);

        if (requestModel.Ejercicio.HasValue)
            polizasQuery = polizasQuery.Where(p => p.Ejercicio == requestModel.Ejercicio);

        if (requestModel.Periodo.HasValue)
            polizasQuery = polizasQuery.Where(p => p.Periodo == requestModel.Periodo);

        if (requestModel.Numero.HasValue)
            polizasQuery = polizasQuery.Where(p => p.Folio == requestModel.Numero);

        List<PolizaSql> polizasSql = await polizasQuery.ProjectTo<PolizaSql>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        foreach (PolizaSql polizaSql in polizasSql)
        {
            var poliza = _mapper.Map<Poliza>(polizaSql);

            await CargarDatosRelacionadosAsync(poliza, polizaSql, loadRelatedDataOptions, cancellationToken);

            polizasList.Add(poliza);
        }

        return polizasList;
    }

    private async Task CargarDatosRelacionadosAsync(Poliza poliza,
                                                    PolizaSql polizaSql,
                                                    ILoadRelatedDataOptions loadRelatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        poliza.Movimientos = (await _movimientoRepository.BuscarPorPolizaIdAsync(polizaSql.Id, loadRelatedDataOptions, cancellationToken))
            .ToList();

        if (loadRelatedDataOptions.CargarDatosExtra)
            poliza.DatosExtra =
                (await _context.Polizas.FirstAsync(m => m.Id == polizaSql.Id, cancellationToken)).ToDatosDictionary<Polizas>();
    }
}
