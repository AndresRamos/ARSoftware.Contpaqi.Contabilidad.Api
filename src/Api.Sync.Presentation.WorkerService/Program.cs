using Api.Sync.Core.Application;
using Api.Sync.Core.Application.ContpaqiContabilidad.Commands.IniciarSdk;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Infrastructure;
using Api.Sync.Presentation.WorkerService;
using MediatR;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationServices(context.Configuration);
        services.AddInfrastructureServices(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext();
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

var sdkSesionService = host.Services.GetRequiredService<ISdkSesionService>();
var mediator = host.Services.GetRequiredService<IMediator>();

await mediator.Send(new IniciarSdkCommand());

await host.RunAsync();

if (sdkSesionService.EmpresaAbierta)
{
    logger.LogInformation("Cerrando empresa.");
    sdkSesionService.CierraEmpresa();
}

if (sdkSesionService.ConexionInciada)
{
    logger.LogInformation("Terminando SDK.");
    sdkSesionService.TerminarConexion();
}
