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

public sealed class CuentaRepository : ICuentaRepository
{
    private readonly IAgrupadorSatRepository _agrupadorSatRepository;
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMonedaRepository _monedaRepository;
    private readonly ISegmentoNegocioRepository _segmentoNegocioRepository;

    public CuentaRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _monedaRepository = new MonedaRepository(context, mapper);
        _segmentoNegocioRepository = new SegmentoNegocioRepository(context, mapper);
        _agrupadorSatRepository = new AgrupadorSatRepository(context, mapper);
    }

    public async Task<Cuenta?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        CuentaSql? cuentaSql = await _context.Cuentas.Where(c => c.Id == id)
            .ProjectTo<CuentaSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (cuentaSql is null)
            return null;

        var cuenta = _mapper.Map<Cuenta>(cuentaSql);

        await CargarDatosRelacionadosAsync(cuenta, cuentaSql, loadRelatedDataOptions, cancellationToken);

        return cuenta;
    }

    public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.Cuentas.AnyAsync(c => c.Codigo == codigo, cancellationToken);
    }

    private async Task CargarDatosRelacionadosAsync(Cuenta cuenta,
                                                    CuentaSql cuentaSql,
                                                    ILoadRelatedDataOptions loadRelatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        if (cuentaSql.IdSegNeg.HasValue)
            cuenta.SegmentoNegocio =
                await _segmentoNegocioRepository.BuscarPorIdAsync(cuentaSql.IdSegNeg.Value, loadRelatedDataOptions, cancellationToken);

        if (cuentaSql.IdMoneda.HasValue)
            cuenta.Moneda = await _monedaRepository.BuscarPorIdAsync(cuentaSql.IdMoneda.Value, loadRelatedDataOptions, cancellationToken) ??
                            new Moneda();

        if (cuentaSql.IdAgrupadorSAT.HasValue)
            cuenta.AgrupadorSat =
                await _agrupadorSatRepository.BuscarPorIdAsync(cuentaSql.IdAgrupadorSAT.Value, loadRelatedDataOptions, cancellationToken);

        if (loadRelatedDataOptions.CargarDatosExtra)
            cuenta.DatosExtra =
                (await _context.Cuentas.FirstAsync(m => m.Id == cuentaSql.Id, cancellationToken)).ToDatosDictionary<Cuentas>();
    }
}
