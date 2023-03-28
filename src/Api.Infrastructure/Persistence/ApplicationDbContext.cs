using Api.Core.Application.Common.Interfaces;
using Api.Core.Domain.Common;
using Api.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApiRequestBase> Requests => Set<ApiRequestBase>();
    public DbSet<ApiResponseBase> Responses => Set<ApiResponseBase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiRequestBase>().UseTphMappingStrategy().ToTable("Requests");
        modelBuilder.Entity<ApiResponseBase>().UseTphMappingStrategy().ToTable("Responses");

        modelBuilder.Entity<ApiRequestBase>().HasOne(r => r.Response).WithOne().HasForeignKey<ApiResponseBase>(r => r.Id);

        ApiRequestConfiguration.ConfigureRequests(modelBuilder);
        ApiResponseConfiguration.ConfigureResponses(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}
