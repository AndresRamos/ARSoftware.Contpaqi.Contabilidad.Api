using System.Reflection;
using Api.Core.Application;
using Api.Core.Application.Requests.Commands.CreateApiRequest;
using Api.Core.Domain.Common;
using Api.Infrastructure;
using Api.Infrastructure.Persistence;
using Api.Presentation.WebApi.Authentication;
using Microsoft.OpenApi.Models;
using Serilog;

Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/api.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true;
        //options.Filters.Add<ApiKeyAuthFilter>();
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.TypeInfoResolver = new PolymorphicTypeResolver());

builder.Services.AddApplicationServices().AddInfrastructureServices(builder.Configuration);

builder.Services.AddScoped<ApiKeyAuthFilter>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "AR Software - CONTPAQi Contabilidad API",
            Version = "v1",
            Description = "API used to create request to be processed in CONTPAQi Contabilidad",
            Contact = new OpenApiContact { Name = "AR Software", Email = "andres@arsoft.net", Url = new Uri("https://www.arsoft.net") }
        });

    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(CreateApiRequestCommand).Assembly.GetName().Name}.xml"));
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(IContpaqiRequest).Assembly.GetName().Name}.xml"));

    c.UseAllOfForInheritance();
    c.UseOneOfForPolymorphism();
});

builder.Host.UseSerilog();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using IServiceScope scope = app.Services.CreateScope();
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initialiser.InitialiseAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
