using Api.SharedKernel.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using ARSoftware.Contpaqi.Contabilidad.Sql.Models.Empresa;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public CuentaRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Cuenta?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        Cuentas? cuentaContabilidad = await _context.Cuentas.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

#if DEBUG
        return null;
#endif

        if (cuentaContabilidad is null)
            return null;

        var cuenta = _mapper.Map<Cuenta>(cuentaContabilidad);

        cuenta.SegmentoNegocio =
            (await _context.SegmentosNegocio.FirstOrDefaultAsync(s => s.Id == cuentaContabilidad.IdSegNeg, cancellationToken))?.Codigo ??
            "";

        cuenta.Moneda = (await _context.Monedas.FirstOrDefaultAsync(s => s.Id == cuentaContabilidad.IdMoneda, cancellationToken))?.Codigo ??
                        "";

        cuenta.AgrupadorSat =
            (await _context.AgrupadoresSAT.FirstOrDefaultAsync(s => s.Id == cuentaContabilidad.IdAgrupadorSAT, cancellationToken))
            ?.Codigo ?? "";

        return cuenta;
    }
}
