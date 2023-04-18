using System.Reflection;
using Api.Sync.Core.Application.Api.Interfaces;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Infrastructure.Api;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using ARSoftware.Contpaqi.Contabilidad.Sql.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api.Sync.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

        serviceCollection.AddContpaqiContabilidadApiServices();

        serviceCollection.AddContpaqiContabilidadServices(configuration);

        return serviceCollection;
    }

    private static IServiceCollection AddContpaqiContabilidadApiServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient<IContpaqiComercialApiService, ContpaqiComercialApiService>((serviceProvider, httpClient) =>
        {
            ApiSyncConfig apiSyncConfig = serviceProvider.GetRequiredService<IOptions<ApiSyncConfig>>().Value;
            ContpaqiContabilidadConfig contpaqiContabilidadConfig =
                serviceProvider.GetRequiredService<IOptions<ContpaqiContabilidadConfig>>().Value;
            httpClient.BaseAddress = new Uri(apiSyncConfig.BaseAddress);
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiSyncConfig.SubscriptionKey);
            httpClient.DefaultRequestHeaders.Add("x-Empresa-Rfc", contpaqiContabilidadConfig.Empresa.Rfc);
        });

        return serviceCollection;
    }

    private static void AddContpaqiContabilidadServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ContpaqiContabilidadGeneralesDbContext>(
            builder =>
            {
                builder.UseSqlServer(ContpaqiContabilidadSqlConnectionStringFactory.CreateContpaqiContabilidadGeneralesConnectionString(
                    configuration.GetConnectionString("Contpaqi")));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        serviceCollection.AddDbContext<ContpaqiContabilidadEmpresaDbContext>((provider, builder) =>
        {
            ContpaqiContabilidadConfig config = provider.GetRequiredService<IOptions<ContpaqiContabilidadConfig>>().Value;

            builder.UseSqlServer(
                ContpaqiContabilidadSqlConnectionStringFactory.CreateContpaqiContabilidadEmpresaConnectionString(
                    configuration.GetConnectionString("Contpaqi"), config.Empresa.BaseDatos));
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        serviceCollection.AddTransient<IAgrupadorSatRepository, AgrupadorSatRepository>();
        serviceCollection.AddTransient<ICuentaRepository, CuentaRepository>();
        serviceCollection.AddTransient<IDiarioEspecialRepository, DiarioEspecialRepository>();
        serviceCollection.AddTransient<IEmpresaRepository, EmpresaRepository>();
        serviceCollection.AddTransient<IMonedaRepository, MonedaRepository>();
        serviceCollection.AddTransient<IPolizaRepository, PolizaRepository>();
        serviceCollection.AddTransient<ISegmentoNegocioRepository, SegmentoNegocioRepository>();
        serviceCollection.AddTransient<ITipoPolizaRepository, TipoPolizaRepository>();
    }
}
