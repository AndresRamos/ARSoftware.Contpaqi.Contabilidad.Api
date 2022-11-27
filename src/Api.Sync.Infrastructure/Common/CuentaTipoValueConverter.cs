using Api.SharedKernel.Models;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Extensions;
using AutoMapper;

namespace Api.Sync.Infrastructure.Common;

public sealed class CuentaTipoValueConverter : IValueConverter<string, CuentaTipo>
{
    public CuentaTipo Convert(string sourceMember, ResolutionContext context)
    {
        return ContabilidadSdkExtensions.ConvertToCuentaTipo(sourceMember);
    }
}
