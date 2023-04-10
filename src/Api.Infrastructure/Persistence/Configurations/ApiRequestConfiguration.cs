using Api.Core.Domain.Requests;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Persistence.Configurations;

public static class ApiRequestConfiguration
{
    public static void ConfigureRequests(ModelBuilder builder)
    {
        builder.Configure<CrearPolizaRequest, CrearPolizaRequestModel, CrearPolizaRequestOptions>();
        builder.Configure<BuscarPolizasRequest, BuscarPolizasRequestModel, BuscarPolizasRequestOptions>();
        builder.Configure<EliminarPolizaRequest, EliminarPolizaRequestModel, EliminarPolizaRequestOptions>();
        builder.Configure<CrearCuentaRequest, CrearCuentaRequestModel, CrearCuentaRequestOptions>();
        builder.Configure<BuscarCuentasRequest, BuscarCuentasRequestModel, BuscarCuentasRequestOptions>();
        builder.Configure<BuscarTiposPolizaRequest, BuscarTiposPolizaRequestModel, BuscarTiposPolizaRequestOptions>();
    }
}
