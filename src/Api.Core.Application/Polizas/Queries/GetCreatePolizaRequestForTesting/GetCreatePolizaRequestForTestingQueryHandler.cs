using Api.SharedKernel.Models;
using Api.SharedKernel.Requests;
using MediatR;

namespace Api.Core.Application.Polizas.Queries.GetCreatePolizaRequestForTesting;

public sealed class
    GetCreatePolizaRequestForTestingQueryHandler : IRequestHandler<GetCreatePolizaRequestForTestingQuery, CreatePolizaRequest>
{
    public Task<CreatePolizaRequest> Handle(GetCreatePolizaRequestForTestingQuery request, CancellationToken cancellationToken)
    {
        var poliza = new Poliza
        {
            Tipo = "3",
            Fecha = DateTime.Today,
            Concepto = "Poliza de prueba",
            Movimientos = new List<Movimiento>
            {
                new()
                {
                    Numero = 1,
                    Tipo = MovimientoTipo.Cargo,
                    Cuenta = "40119000",
                    Importe = 100,
                    Referencia = "Referencia",
                    Concepto = "Concepto",
                    Diario = ""
                },
                new()
                {
                    Numero = 1,
                    Tipo = MovimientoTipo.Abono,
                    Cuenta = "60101000",
                    Importe = 100,
                    Referencia = "Referencia",
                    Concepto = "Concepto",
                    Diario = ""
                }
            }
        };

        var options = new CreatePolizaOptions { BuscarSiguienteNumero = true };

        return Task.FromResult(new CreatePolizaRequest { Model = poliza, Options = options });
    }
}
