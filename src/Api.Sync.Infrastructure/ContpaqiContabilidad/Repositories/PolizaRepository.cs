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
