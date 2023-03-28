using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
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

    public PolizaRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Poliza?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        Poliza? poliza = await _context.Polizas.Where(p => p.Id == id)
            .ProjectTo<Poliza>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (poliza != null)
        {
            List<MovimientosPoliza> movimietosContabilidad = await _context.MovimientosPoliza.Where(m => m.IdPoliza == id)
                .ToListAsync(cancellationToken);

            foreach (MovimientosPoliza mov in movimietosContabilidad)
            {
                var movimiento = _mapper.Map<Movimiento>(mov);
                movimiento.Cuenta = (await _context.Cuentas.FirstOrDefaultAsync(c => c.Id == mov.IdCuenta, cancellationToken))?.Codigo ??
                                    "";

                movimiento.SegmentoNegocio =
                    (await _context.SegmentosNegocio.FirstOrDefaultAsync(c => c.Id == mov.IdSegNeg, cancellationToken))?.Codigo ?? "";

                movimiento.Diario = (await _context.DiariosEspeciales.FirstOrDefaultAsync(c => c.Id == mov.IdDiario, cancellationToken))
                                    ?.Codigo ??
                                    "";

                poliza.Movimientos.Add(movimiento);
            }
        }

        return poliza;
    }
}
