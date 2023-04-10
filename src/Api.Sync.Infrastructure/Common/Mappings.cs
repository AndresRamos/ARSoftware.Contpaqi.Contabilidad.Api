using Api.Core.Domain.Models;
using Api.Sync.Infrastructure.ContpaqiContabilidad.Models;
using ARSoftware.Contpaqi.Contabilidad.Sql.Models.Empresa;
using ARSoftware.Contpaqi.Contabilidad.Sql.Models.Generales;
using AutoMapper;

namespace Api.Sync.Infrastructure.Common;

public sealed class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<AgrupadoresSAT, AgrupadorSatSql>();
        CreateMap<AgrupadorSatSql, AgrupadorSat>()
            .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.Codigo))
            .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(des => des.Tipo, opt => opt.MapFrom(src => src.Tipo));

        CreateMap<Cuentas, CuentaSql>();
        CreateMap<CuentaSql, Cuenta>()
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

        CreateMap<DiariosEspeciales, DiarioEspecialSql>();
        CreateMap<DiarioEspecialSql, DiarioEspecial>()
            .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.Codigo))
            .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(des => des.Tipo, opt => opt.MapFrom(src => src.Tipo));

        CreateMap<ListaEmpresas, EmpresaSql>();
        CreateMap<EmpresaSql, Empresa>()
            .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(des => des.BaseDatos, opt => opt.MapFrom(src => src.AliasBDD));

        CreateMap<Monedas, MonedaSql>();
        CreateMap<MonedaSql, Moneda>()
            .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.Codigo))
            .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(des => des.CodigoSat, opt => opt.MapFrom(src => src.CodigoSAT));

        CreateMap<MovimientosPoliza, MovimientoSql>();
        CreateMap<MovimientoSql, Movimiento>()
            .ForMember(des => des.Numero, opt => opt.MapFrom(src => src.NumMovto))
            .ForMember(des => des.Tipo, opt => opt.MapFrom(src => src.TipoMovto == true ? 2 : 1))
            .ForMember(des => des.Importe, opt => opt.MapFrom(src => src.Importe))
            .ForMember(des => des.Referencia, opt => opt.MapFrom(src => src.Referencia))
            .ForMember(des => des.Concepto, opt => opt.MapFrom(src => src.Concepto));

        CreateMap<Parametros, ParametrosSql>();

        CreateMap<Polizas, PolizaSql>();
        CreateMap<PolizaSql, Poliza>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.Fecha, opt => opt.MapFrom(src => src.Fecha))
            .ForMember(des => des.Tipo, opt => opt.MapFrom(src => src.TipoPol))
            .ForMember(des => des.Numero, opt => opt.MapFrom(src => src.Folio))
            .ForMember(des => des.Concepto, opt => opt.MapFrom(src => src.Concepto));

        CreateMap<SegmentosNegocio, SegmentoNegocioSql>();
        CreateMap<SegmentoNegocioSql, SegmentoNegocio>()
            .ForMember(des => des.Codigo, opt => opt.MapFrom(src => src.Codigo))
            .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.Nombre));

        CreateMap<TiposPolizas, TipoPolizaSql>();
        CreateMap<TipoPolizaSql, TipoPoliza>()
            .ForMember(des => des.Codigo, opt => opt.MapFrom(src => int.Parse(src.Codigo)))
            .ForMember(des => des.Nombre, opt => opt.MapFrom(src => src.Nombre));
    }
}
