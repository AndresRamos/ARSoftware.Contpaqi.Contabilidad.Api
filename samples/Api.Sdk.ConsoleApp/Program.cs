﻿using Api.Sdk.ConsoleApp.JsonFactories;

Console.WriteLine("Programa inicio.");

const string baseDirectory = @"C:\AR Software\Contpaqi Contabilidad API\Requests";

if (Directory.Exists(baseDirectory))
    Directory.Delete(baseDirectory, true);

PolizaFactory.CearJson(Path.Combine(baseDirectory, "Polizas"));
CuentasFactory.CearJson(Path.Combine(baseDirectory, "Cuentas"));

Console.WriteLine("Programa fin.");
