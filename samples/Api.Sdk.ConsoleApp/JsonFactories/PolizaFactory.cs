﻿using System.Text.Json;
using Api.Core.Domain.Models;
using Api.Core.Domain.Models.Enums;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class PolizaFactory
{
    private static CrearPolizaRequest Crear()
    {
        CrearPolizaRequest request = InicializarCrearPolizaRequest();

        request.Model.Poliza = new Poliza
        {
            Tipo = new TipoPoliza { Codigo = 3 },
            Concepto = "Concepto Poliza",
            Fecha = DateTime.Today,
            Movimientos = new List<Movimiento>
            {
                new()
                {
                    Numero = 1,
                    Tipo = TipoMovimiento.Abono,
                    Cuenta = new Cuenta { Codigo = "60101000" },
                    Importe = 100,
                    Referencia = "Referencia",
                    Concepto = "Concepto"
                },
                new()
                {
                    Numero = 2,
                    Tipo = TipoMovimiento.Cargo,
                    Cuenta = new Cuenta { Codigo = "40119000" },
                    Importe = 100,
                    Referencia = "Referencia",
                    Concepto = "Concepto"
                }
            }
        };

        return request;
    }

    private static BuscarPolizasRequest BuscarPorId()
    {
        BuscarPolizasRequest request = InicializarBuscarPolizasRequest();
        request.Model.Id = 1;
        return request;
    }

    private static BuscarPolizasRequest BuscarPorRangoFecha()
    {
        BuscarPolizasRequest request = InicializarBuscarPolizasRequest();
        request.Model.Tipo = 3;
        request.Model.FechaInicio = DateOnly.FromDateTime(DateTime.Today);
        request.Model.FechaFin = DateOnly.FromDateTime(DateTime.Today);
        return request;
    }

    private static BuscarPolizasRequest BuscarPorNumero()
    {
        BuscarPolizasRequest request = InicializarBuscarPolizasRequest();
        request.Model.Tipo = 3;
        request.Model.Ejercicio = 2023;
        request.Model.Periodo = 3;
        request.Model.Numero = 24;
        return request;
    }

    private static BuscarPolizasRequest BuscarPorSql()
    {
        BuscarPolizasRequest request = InicializarBuscarPolizasRequest();
        request.Model.SqlQuery = "Cargos = 100";
        return request;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearPolizaRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarPolizasRequest)}_PorId.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarPolizasRequest)}_PorRangoFecha.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorRangoFecha(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarPolizasRequest)}_PorNumero.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorNumero(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarPolizasRequest)}_PorSql.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorSql(), options));
    }

    private static CrearPolizaRequest InicializarCrearPolizaRequest()
    {
        var request = new CrearPolizaRequest(new CrearPolizaRequestModel(), new CrearPolizaRequestOptions());
        return request;
    }

    private static BuscarPolizasRequest InicializarBuscarPolizasRequest()
    {
        var request = new BuscarPolizasRequest(new BuscarPolizasRequestModel(), new BuscarPolizasRequestOptions());
        return request;
    }
}
