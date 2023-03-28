using System.Reflection;
using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Common.Behaviors;
using Api.Sync.Core.Application.Common.Models;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Core.Application.ContpaqiContabilidad.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDKCONTPAQNGLib;

namespace Api.Sync.Core.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
        serviceCollection.AddMediatR(serviceConfiguration =>
        {
            serviceConfiguration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            serviceConfiguration.RegisterServicesFromAssemblyContaining<ApiRequestBase>();
            serviceConfiguration.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
        });

        serviceCollection.Configure<ApiSyncConfig>(configuration.GetSection(nameof(ApiSyncConfig)));
        serviceCollection.Configure<ContpaqiContabilidadConfig>(configuration.GetSection(nameof(ContpaqiContabilidadConfig)));

        serviceCollection.AddContpaqiContabilidadSdkServices();

        return serviceCollection;
    }

    private static void AddContpaqiContabilidadSdkServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ISdkSesionService, SdkSesionService>();

        serviceCollection.AddSingleton(new TSdkSesion());
        serviceCollection.AddSingleton(provider =>
        {
            var sdkSesion = provider.GetRequiredService<TSdkSesion>();
            var sdkPoliza = new TSdkPoliza();
            sdkPoliza.setSesion(sdkSesion);
            return sdkPoliza;
        });
        serviceCollection.AddSingleton(provider =>
        {
            var sdkSesion = provider.GetRequiredService<TSdkSesion>();
            var sdkMovimientoPoliza = new TSdkMovimientoPoliza();
            sdkMovimientoPoliza.setSesion(sdkSesion);
            return sdkMovimientoPoliza;
        });
        serviceCollection.AddSingleton(provider =>
        {
            var sdkSesion = provider.GetRequiredService<TSdkSesion>();
            var sdkAsocCfdi = new TSdkAsocCFDI();
            sdkAsocCfdi.setSesion(sdkSesion);
            return sdkAsocCfdi;
        });
        serviceCollection.AddSingleton(provider =>
        {
            var sdkSesion = provider.GetRequiredService<TSdkSesion>();
            var sdkCuenta = new TSdkCuenta();
            sdkCuenta.setSesion(sdkSesion);
            return sdkCuenta;
        });
    }
}
