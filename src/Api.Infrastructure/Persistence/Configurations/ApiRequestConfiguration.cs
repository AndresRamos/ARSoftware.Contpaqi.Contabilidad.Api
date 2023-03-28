using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Configurations;

public static class ApiRequestConfiguration
{
    public static void ConfigureRequests(ModelBuilder builder)
    {
        builder.Configure<CrearPolizaRequest, CrearPolizaRequestModel, CrearPolizaRequestOptions>();
        builder.Configure<CrearCuentaRequest, CrearCuentaRequestModel, CrearCuentaRequestOptions>();
    }
}
