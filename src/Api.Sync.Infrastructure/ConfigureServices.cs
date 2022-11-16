using System.Reflection;
using Api.Sync.Core.Application.Api.Interfaces;
using Api.Sync.Core.Application.Common;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Infrastructure.Api;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Repositories;
using ARSoftware.Contpaqi.Contabilidad.Sql.Contexts;
using Microsoft.Data.SqlClient;
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

        serviceCollection.AddHttpClient("ApiClient", (provider, client) =>
        {
            ApiConfig config = provider.GetRequiredService<IOptions<ApiConfig>>().Value;
            client.BaseAddress = new Uri(config.Endpoint);
        });

        serviceCollection.AddTransient<IRequestRepository, RequestRepository>();

        serviceCollection.AddContpaqiContabilidadServices(configuration);

        return serviceCollection;
    }

    private static void AddContpaqiContabilidadServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ContpaqiContabilidadGeneralesDbContext>(builder =>
        {
            string? connectionString = configuration.GetConnectionString("Contpaqi");
            builder.UseSqlServer(connectionString);
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        serviceCollection.AddDbContext<ContpaqiContabilidadEmpresaDbContext>((provider, builder) =>
        {
            ContabilidadConfig config = provider.GetRequiredService<IOptions<ContabilidadConfig>>().Value;

            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("Contpaqi"))
            {
                InitialCatalog = config.Empresa
            };

            builder.UseSqlServer(sqlConnectionStringBuilder.ToString());
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        serviceCollection.AddTransient<IEmpresaRepository, EmpresaRepository>();
        serviceCollection.AddTransient<IPolizaRepository, PolizaRepository>();
        serviceCollection.AddTransient<ICuentaRepository, CuentaRepository>();
    }
}
