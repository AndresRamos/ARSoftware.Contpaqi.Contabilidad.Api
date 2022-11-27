using Api.SharedKernel.Models;
using Api.SharedKernel.Requests;
using MediatR;

namespace Api.Core.Application.Cuentas.Queries.GetCreateCuentaRequestForTesting;

public sealed class
    GetCreateCuentaRequestForTestingQueryHandler : IRequestHandler<GetCreateCuentaRequestForTestingQuery, CreateCuentaRequest>
{
    public Task<CreateCuentaRequest> Handle(GetCreateCuentaRequestForTestingQuery request, CancellationToken cancellationToken)
    {
        var cuenta = new Cuenta
        {
            CodigoCuentaAcumula = "10201000",
            Codigo = "10201001",
            Nombre = "BANCOMER",
            NombreOtroIdioma = "BANCOMER",
            Tipo = CuentaTipo.ActivoDeudora,
            CuentaDeMayor = CuentaDeMayor.No,
            SegmentoNegocioEnMovimientos = false,
            Moneda = "1",
            DigitoAgrupador = 0,
            AgrupadorSat = "102.01"
        };

        return Task.FromResult(new CreateCuentaRequest { Model = cuenta });
    }
}
