using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Configurations;

public static class ApiResponseConfiguration
{
    public static void ConfigureResponses(ModelBuilder builder)
    {
        builder.Configure<CrearPolizaResponse, CrearPolizaResponseModel>();
        builder.Configure<BuscarPolizasResponse, BuscarPolizasResponseModel>();
        builder.Configure<EliminarPolizaResponse, EliminarPolizaResponseModel>();
        builder.Configure<CrearCuentaResponse, CrearCuentaResponseModel>();
        builder.Configure<BuscarCuentasResponse, BuscarCuentasResponseModel>();
    }
}
