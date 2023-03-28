using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Contabilidad.Sql.Models.Empresa;
using AutoMapper;

namespace Api.Sync.Infrastructure.Common;

public sealed class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<Polizas, Poliza>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.Fecha, opt => opt.MapFrom(src => src.Fecha))
            .ForMember(des => des.Tipo, opt => opt.MapFrom(src => src.TipoPol))
            .ForMember(des => des.Numero, opt => opt.MapFrom(src => src.Folio))
            .ForMember(des => des.Concepto, opt => opt.MapFrom(src => src.Concepto));

        CreateMap<MovimientosPoliza, Movimiento>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.Numero, opt => opt.MapFrom(src => src.NumMovto))
            .ForMember(des => des.Tipo, opt => opt.MapFrom(src => src.TipoMovto == true ? 2 : 1))
            .ForMember(des => des.Importe, opt => opt.MapFrom(src => src.Importe))
            .ForMember(des => des.Referencia, opt => opt.MapFrom(src => src.Referencia))
            .ForMember(des => des.Concepto, opt => opt.MapFrom(src => src.Concepto));

        CreateMap<Cuentas, Cuenta>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.Codigo))
            .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(des => des.NombreOtroIdioma, opt => opt.MapFrom(src => src.NomIdioma))
            .ForMember(des => des.Tipo, opt => opt.ConvertUsing(new CuentaTipoValueConverter(), src => src.Tipo))
            .ForMember(des => des.CuentaDeMayor, opt => opt.ConvertUsing(new CuentaDeMayorValueConverter(), src => src.CtaMayor))
            .ForMember(des => des.SegmentoNegocioEnMovimientos, opt => opt.MapFrom(src => src.SegNegMovtos))
            .ForMember(des => des.DigitoAgrupador, opt => opt.MapFrom(src => src.DigAgrup))
            .ForMember(des => des.FechaAlta, opt => opt.MapFrom(src => src.FechaRegistro))
            .ForMember(des => des.EsBaja, opt => opt.MapFrom(src => src.EsBaja));
    }
}
