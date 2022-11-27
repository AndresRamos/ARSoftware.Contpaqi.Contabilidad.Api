using System.Reflection;
using Api.Sync.Core.Application.Common;
using Api.Sync.Core.Application.Common.Behaviors;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Core.Application.ContpaqiContabilidad.Services;
using FluentValidation;
using MediatR;
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
        serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        serviceCollection.Configure<ApiConfig>(configuration.GetSection(nameof(ApiConfig)));
        serviceCollection.Configure<ContabilidadConfig>(configuration.GetSection(nameof(ContabilidadConfig)));

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
