using System.Text.Json;
using Api.Core.Application.Common;
using Api.SharedKernel.Common;
using Api.SharedKernel.Models;
using Api.SharedKernel.Requests;
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

        modelBuilder.Entity<CreatePolizaRequest>()
            .Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<Poliza>(p, JsonSerializerOptions.Default))
            .HasColumnName("Model");

        modelBuilder.Entity<CreatePolizaRequest>()
            .Property(m => m.Options)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<CreatePolizaOptions>(p, JsonSerializerOptions.Default));

        modelBuilder.Entity<CreatePolizaResponse>()
            .Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<Poliza>(p, JsonSerializerOptions.Default))
            .HasColumnName("Model");

        modelBuilder.Entity<CreateCuentaRequest>()
            .Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<Cuenta>(p, JsonSerializerOptions.Default))
            .HasColumnName("Model");

        modelBuilder.Entity<CreateCuentaResponse>()
            .Property(m => m.Model)
            .HasConversion(p => JsonSerializer.Serialize(p, JsonSerializerOptions.Default),
                p => JsonSerializer.Deserialize<Cuenta>(p, JsonSerializerOptions.Default))
            .HasColumnName("Model");
    }
}
