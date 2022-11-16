using System.Text.Json;
using Api.Core.Application.Common;
using Api.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Request> Requests => Set<Request>();
    public DbSet<Response> Responses => Set<Response>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Request>().UseTphMappingStrategy();
        modelBuilder.Entity<Response>().UseTphMappingStrategy();

        modelBuilder.Entity<Request>().HasOne(r => r.Response).WithOne().HasForeignKey<Response>(r => r.Id);

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
