using Api.SharedKernel.Requests;
using MediatR;

namespace Api.Core.Application.Cuentas.Queries.GetCreateCuentaRequestForTesting;

public sealed class GetCreateCuentaRequestForTestingQuery : IRequest<CreateCuentaRequest>
{
}
