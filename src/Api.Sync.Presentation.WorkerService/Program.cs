using System.Reflection;
using Api.Sync.Core.Application;
using Api.Sync.Core.Application.ContpaqiContabilidad.Commands.IniciarSdk;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using Api.Sync.Infrastructure;
using Api.Sync.Presentation.WorkerService;
using MediatR;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
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

var mediator = host.Services.GetRequiredService<IMediator>();
var sdkSesionService = host.Services.GetRequiredService<ISdkSesionService>();

await mediator.Send(new IniciarSdkCommand());

host.Run();

logger.LogInformation("Empresa Abierta: {EmpresaAbierta}", sdkSesionService.EmpresaAbierta);
if (sdkSesionService.EmpresaAbierta)
{
    logger.LogInformation("Cerrando empresa.");
    sdkSesionService.CierraEmpresa();
}

logger.LogInformation("Conexion Inciada: {ConexionInciada}", sdkSesionService.ConexionInciada);
if (sdkSesionService.ConexionInciada)
{
    logger.LogInformation("Terminando SDK.");
    sdkSesionService.TerminarConexion();
}
