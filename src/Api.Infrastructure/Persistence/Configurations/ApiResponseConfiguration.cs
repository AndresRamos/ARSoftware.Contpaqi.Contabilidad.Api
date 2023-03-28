using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Configurations;

public static class ApiResponseConfiguration
{
    public static void ConfigureResponses(ModelBuilder builder)
    {
        builder.Configure<CrearPolizaResponse, CrearPolizaResponseModel>();
        builder.Configure<CrearCuentaResponse, CrearCuentaResponseModel>();
    }
}
