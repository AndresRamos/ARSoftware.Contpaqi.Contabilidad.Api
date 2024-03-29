﻿using Api.Sdk.ConsoleApp.JsonFactories;

Console.WriteLine("Programa inicio.");

const string baseDirectory = @"C:\AR Software\Contpaqi Contabilidad API\Requests";

if (Directory.Exists(baseDirectory))
    Directory.Delete(baseDirectory, true);

PolizaFactory.CearJson(Path.Combine(baseDirectory, "Polizas"));
CuentasFactory.CearJson(Path.Combine(baseDirectory, "Cuentas"));
TiposPolizaFactory.CearJson(Path.Combine(baseDirectory, "TiposPoliza"));
DiariosEspecialesFactory.CearJson(Path.Combine(baseDirectory, "DiariosEspeciales"));
SegmentosNegocioFactory.CearJson(Path.Combine(baseDirectory, "SegmentosNegocio"));

Console.WriteLine("Programa fin.");
