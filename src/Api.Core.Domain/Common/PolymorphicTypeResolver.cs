using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Api.Core.Domain.Requests;

namespace Api.Core.Domain.Common;

public sealed class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        Type apiRequestBaseType = typeof(ApiRequestBase);
        if (jsonTypeInfo.Type == apiRequestBaseType)
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$type",
                IgnoreUnrecognizedTypeDiscriminators = true,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(CrearPolizaRequest), nameof(CrearPolizaRequest)),
                    new JsonDerivedType(typeof(CrearCuentaRequest), nameof(CrearCuentaRequest)),
                    new JsonDerivedType(typeof(BuscarCuentasRequest), nameof(BuscarCuentasRequest)),
                    new JsonDerivedType(typeof(BuscarPolizasRequest), nameof(BuscarPolizasRequest)),
                    new JsonDerivedType(typeof(BuscarTiposPolizaRequest), nameof(BuscarTiposPolizaRequest)),
                    new JsonDerivedType(typeof(BuscarEmpresasRequest), nameof(BuscarEmpresasRequest)),
                    new JsonDerivedType(typeof(BuscarSegmentosNegocioRequest), nameof(BuscarSegmentosNegocioRequest)),
                    new JsonDerivedType(typeof(BuscarDiariosEspecialesRequest), nameof(BuscarDiariosEspecialesRequest))
                }
            };

        Type apiResponseBaseType = typeof(ApiResponseBase);
        if (jsonTypeInfo.Type == apiResponseBaseType)
            jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
            {
                TypeDiscriminatorPropertyName = "$type",
                IgnoreUnrecognizedTypeDiscriminators = true,
                UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                DerivedTypes =
                {
                    new JsonDerivedType(typeof(CrearPolizaResponse), nameof(CrearPolizaResponse)),
                    new JsonDerivedType(typeof(CrearCuentaResponse), nameof(CrearCuentaResponse)),
                    new JsonDerivedType(typeof(BuscarCuentasResponse), nameof(BuscarCuentasResponse)),
                    new JsonDerivedType(typeof(BuscarPolizasResponse), nameof(BuscarPolizasResponse)),
                    new JsonDerivedType(typeof(BuscarEmpresasResponse), nameof(BuscarEmpresasResponse)),
                    new JsonDerivedType(typeof(BuscarSegmentosNegocioResponse), nameof(BuscarSegmentosNegocioResponse)),
                    new JsonDerivedType(typeof(BuscarDiariosEspecialesResponse), nameof(BuscarDiariosEspecialesResponse))
                }
            };

        return jsonTypeInfo;
    }
}
