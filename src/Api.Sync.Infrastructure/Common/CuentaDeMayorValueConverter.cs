using Api.Core.Domain.Models;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Extensions;
using AutoMapper;

namespace Api.Sync.Infrastructure.Common;

public sealed class CuentaDeMayorValueConverter : IValueConverter<int?, CuentaDeMayor>
{
    public CuentaDeMayor Convert(int? sourceMember, ResolutionContext context)
    {
        return ContabilidadSdkExtensions.ConvertToCuentaDeMayor(sourceMember);
    }
}
