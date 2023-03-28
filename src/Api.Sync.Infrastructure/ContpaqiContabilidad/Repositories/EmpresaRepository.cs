using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Extensions;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Models;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using ARSoftware.Contpaqi.Contabilidad.Sql.Factories;
using ARSoftware.Contpaqi.Contabilidad.Sql.Models.Empresa;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;

public sealed class EmpresaRepository : IEmpresaRepository
{
    private readonly IConfiguration _configuration;
    private readonly ContpaqiContabilidadEmpresaDbContext _empresaContext;
    private readonly ContpaqiContabilidadGeneralesDbContext _generalesContext;
    private readonly IMapper _mapper;

    public EmpresaRepository(ContpaqiContabilidadGeneralesDbContext generalesContext,
                             IMapper mapper,
                             IConfiguration configuration,
                             ContpaqiContabilidadEmpresaDbContext empresaContext)
    {
        _generalesContext = generalesContext;
        _mapper = mapper;
        _configuration = configuration;
        _empresaContext = empresaContext;
    }

    public async Task<Empresa?> BuscarPorRfcAsync(string rfc,
                                                  ILoadRelatedDataOptions loadRelatedDataOptions,
                                                  CancellationToken cancellationToken)
    {
        IEnumerable<Empresa> empresas = await BuscarTodoAsync(loadRelatedDataOptions, cancellationToken);

        return empresas.FirstOrDefault(e => e.Rfc == rfc);
    }

    public async Task<IEnumerable<Empresa>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions,
                                                            CancellationToken cancellationToken)
    {
        var empresasList = new List<Empresa>();

        List<EmpresaSql> empresasSql = await _generalesContext.ListaEmpresas.ProjectTo<EmpresaSql>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        foreach (EmpresaSql? empresaSql in empresasSql)
        {
            var empresa = _mapper.Map<Empresa>(empresaSql);

            await CargarDatosRelacionadosAsync(empresa, loadRelatedDataOptions, cancellationToken);

            empresasList.Add(empresa);
        }

        return empresasList;
    }

    private async Task CargarDatosRelacionadosAsync(Empresa empresa,
                                                    ILoadRelatedDataOptions relatedDataOptions,
                                                    CancellationToken cancellationToken)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ContpaqiContabilidadEmpresaDbContext>();
        string empresaConnectionString = ContpaqiContabilidadSqlConnectionStringFactory.CreateContpaqiContabilidadEmpresaConnectionString(
            _configuration.GetConnectionString("Contpaqi"),
            empresa.BaseDatos);
        optionsBuilder.UseSqlServer(empresaConnectionString);

        _empresaContext.Database.SetConnectionString(empresaConnectionString);

        ParametrosSql? parametrosSql = await _empresaContext.Parametros.ProjectTo<ParametrosSql>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);

        empresa.GuidAdd = parametrosSql.GuidDSL;
        empresa.Rfc = parametrosSql.RFC;

        if (relatedDataOptions.CargarDatosExtra)
            empresa.DatosExtra = (await _empresaContext.Parametros.FirstAsync(cancellationToken)).ToDatosDictionary<Parametros>();
    }
}
