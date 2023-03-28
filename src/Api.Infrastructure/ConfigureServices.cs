using Api.Core.Application.Common.Interfaces;
using Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString(nameof(ApplicationDbContext));

            options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        serviceCollection.AddScoped<ApplicationDbContextInitialiser>();

        return serviceCollection;
    }
}
