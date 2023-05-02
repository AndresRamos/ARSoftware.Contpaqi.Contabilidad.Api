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

public sealed class MonedaRepository : IMonedaRepository
{
    private readonly ContpaqiContabilidadEmpresaDbContext _context;
    private readonly IMapper _mapper;

    public MonedaRepository(ContpaqiContabilidadEmpresaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        return await _context.Monedas.AnyAsync(c => c.Codigo.Trim() == codigo.Trim(), cancellationToken);
    }

    public async Task<Moneda?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken)
    {
        MonedaSql? monedaSql = await _context.Monedas.Where(s => s.Id == id)
            .ProjectTo<MonedaSql>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (monedaSql is null)
            return null;

        var moneda = _mapper.Map<Moneda>(monedaSql);

        await CargarDatosRelacionadosAsync(moneda, monedaSql, loadRelatedDataOptions, cancellationToken);

        return moneda;
    }

    private async Task CargarDatosRelacionadosAsync(Moneda moneda, MonedaSql monedaSql, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken)
    {
        if (loadRelatedDataOptions.CargarDatosExtra)
            moneda.DatosExtra =
                (await _context.Monedas.FirstAsync(m => m.Id == monedaSql.Id, cancellationToken)).ToDatosDictionary<Monedas>();
    }
}
