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

public sealed class MovimientoRepository : IMovimientoRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IDiarioEspecialRepository _diarioEspecialRepository;
    private readonly IMapper _mapper;
    private readonly ISegmentoNegocioRepository _segmentoNegocioRepository;

    public MovimientoRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _cuentaRepository = new CuentaRepository(context, mapper);
        _segmentoNegocioRepository = new SegmentoNegocioRepository(context, mapper);
        _diarioEspecialRepository = new DiarioEspecialRepository(context, mapper);
    }

    public async Task<IEnumerable<Movimiento>> BuscarPorPolizaIdAsync(int polizaId, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        var movimientosList = new List<Movimiento>();

        List<MovimientoSql> movimientosSql = await _context.MovimientosPoliza.Where(m => m.IdPoliza == polizaId)
            .ProjectTo<MovimientoSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (MovimientoSql movimientoSql in movimientosSql)
        {
            var movimiento = _mapper.Map<Movimiento>(movimientoSql);

            await CargarDatosRelacionadosAsync(movimiento, movimientoSql, loadRelatedDataOptions, cancellationToken);

            movimientosList.Add(movimiento);
        }

        return movimientosList;
    }

    private async Task CargarDatosRelacionadosAsync(Movimiento movimiento, MovimientoSql movimientoSql,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        movimiento.Cuenta = await _cuentaRepository.BuscarPorIdAsync(movimientoSql.IdCuenta, loadRelatedDataOptions, cancellationToken) ??
                            new Cuenta();

        if (movimientoSql.IdSegNeg.HasValue)
            movimiento.SegmentoNegocio =
                await _segmentoNegocioRepository.BuscarPorIdAsync(movimientoSql.IdSegNeg.Value, loadRelatedDataOptions, cancellationToken);

        if (movimientoSql.IdDiario.HasValue)
            movimiento.Diario =
                await _diarioEspecialRepository.BuscarPorIdAsync(movimientoSql.IdDiario.Value, loadRelatedDataOptions, cancellationToken);

        if (loadRelatedDataOptions.CargarDatosExtra)
            movimiento.DatosExtra = (await _context.MovimientosPoliza.FirstAsync(m => m.Id == movimientoSql.Id, cancellationToken))
                .ToDatosDictionary<MovimientosPoliza>();
    }
}
