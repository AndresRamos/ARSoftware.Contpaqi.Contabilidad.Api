﻿// <auto-generated />
using System;
using Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api.Core.Domain.Common.ApiRequestBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpresaRfc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("SubscriptionKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Requests", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApiRequestBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Api.Core.Domain.Common.ApiResponseBase", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ExecutionTime")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Responses", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApiResponseBase");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.BuscarCuentasRequest", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiRequestBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.Property<string>("Options")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Options");

                    b.HasDiscriminator().HasValue("BuscarCuentasRequest");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.BuscarPolizasRequest", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiRequestBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.Property<string>("Options")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Options");

                    b.HasDiscriminator().HasValue("BuscarPolizasRequest");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.CrearCuentaRequest", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiRequestBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.Property<string>("Options")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Options");

                    b.HasDiscriminator().HasValue("CrearCuentaRequest");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.CrearPolizaRequest", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiRequestBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.Property<string>("Options")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Options");

                    b.HasDiscriminator().HasValue("CrearPolizaRequest");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.EliminarPolizaRequest", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiRequestBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.Property<string>("Options")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Options");

                    b.HasDiscriminator().HasValue("EliminarPolizaRequest");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.BuscarCuentasResponse", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiResponseBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.HasDiscriminator().HasValue("BuscarCuentasResponse");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.BuscarPolizasResponse", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiResponseBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.HasDiscriminator().HasValue("BuscarPolizasResponse");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.CrearCuentaResponse", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiResponseBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.HasDiscriminator().HasValue("CrearCuentaResponse");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.CrearPolizaResponse", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiResponseBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.HasDiscriminator().HasValue("CrearPolizaResponse");
                });

            modelBuilder.Entity("Api.Core.Domain.Requests.EliminarPolizaResponse", b =>
                {
                    b.HasBaseType("Api.Core.Domain.Common.ApiResponseBase");

                    b.Property<string>("Model")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Model");

                    b.HasDiscriminator().HasValue("EliminarPolizaResponse");
                });

            modelBuilder.Entity("Api.Core.Domain.Common.ApiResponseBase", b =>
                {
                    b.HasOne("Api.Core.Domain.Common.ApiRequestBase", null)
                        .WithOne("Response")
                        .HasForeignKey("Api.Core.Domain.Common.ApiResponseBase", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Api.Core.Domain.Common.ApiRequestBase", b =>
                {
                    b.Navigation("Response");
                });
#pragma warning restore 612, 618
        }
    }
}
