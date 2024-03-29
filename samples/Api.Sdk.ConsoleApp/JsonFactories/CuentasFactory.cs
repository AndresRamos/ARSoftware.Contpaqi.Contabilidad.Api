﻿using System.Text.Json;
using Api.Core.Domain.Models;
using Api.Core.Domain.Models.Enums;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class CuentasFactory
{
    private static CrearCuentaRequest Crear()
    {
        CrearCuentaRequest request = IniciarlizarCrearCuentaRequest();
        request.Model.Cuenta = new Cuenta
        {
            Codigo = "10201001",
            Nombre = "BANCOMER",
            NombreOtroIdioma = "BANCOMER",
            CodigoCuentaAcumula = "10201000",
            Tipo = TipoCuenta.ActivoDeudora,
            CuentaDeMayor = CuentaDeMayor.No,
            SegmentoNegocioEnMovimientos = false,
            SegmentoNegocio = null,
            Moneda = new Moneda { Codigo = "1" },
            DigitoAgrupador = 0,
            AgrupadorSat = new AgrupadorSat { Codigo = "102.01" },
            FechaAlta = DateTime.Today,
            EsBaja = false
        };

        return request;
    }

    private static BuscarCuentasRequest BuscarPorId()
    {
        BuscarCuentasRequest request = IniciarlizarBuscarCuentasRequest();
        request.Model.Id = 13;

        return request;
    }

    private static BuscarCuentasRequest BuscarPorCodigo()
    {
        BuscarCuentasRequest request = IniciarlizarBuscarCuentasRequest();
        request.Model.Codigo = "10100000";

        return request;
    }

    private static BuscarCuentasRequest BuscarPorSql()
    {
        BuscarCuentasRequest request = IniciarlizarBuscarCuentasRequest();
        request.Model.SqlQuery = "Nombre = 'Caja'";

        return request;
    }

    private static BuscarCuentasRequest BuscarTodo()
    {
        BuscarCuentasRequest request = IniciarlizarBuscarCuentasRequest();

        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearCuentaRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_Todo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorId.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarCuentasRequest)}_PorSql.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorSql(), options));
    }

    private static BuscarCuentasRequest IniciarlizarBuscarCuentasRequest()
    {
        var request = new BuscarCuentasRequest(new BuscarCuentasRequestModel(), new BuscarCuentasRequestOptions());

        return request;
    }

    private static CrearCuentaRequest IniciarlizarCrearCuentaRequest()
    {
        var request = new CrearCuentaRequest(new CrearCuentaRequestModel(), new CrearCuentaRequestOptions());

        return request;
    }
}
