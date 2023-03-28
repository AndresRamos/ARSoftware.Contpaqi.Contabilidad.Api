using Api.Core.Domain.Models.Enums;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Extensions;
using AutoMapper;

namespace Api.Sync.Infrastructure.Common;

public sealed class CuentaTipoValueConverter : IValueConverter<string, TipoCuenta>
{
    public TipoCuenta Convert(string sourceMember, ResolutionContext context)
    {
        return ContabilidadSdkExtensions.ConvertToCuentaTipo(sourceMember);
    }
}
