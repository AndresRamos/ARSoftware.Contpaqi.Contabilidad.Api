using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Cuentas;

public sealed record BuscarCuentasRequestHandler : IRequestHandler<BuscarCuentasRequest, BuscarCuentasResponse>
{
    private readonly ICuentaRepository _cuentaRepository;

    public BuscarCuentasRequestHandler(ICuentaRepository cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    public async Task<BuscarCuentasResponse> Handle(BuscarCuentasRequest request, CancellationToken cancellationToken)
    {
        List<Cuenta> cuentas = (await _cuentaRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
            .ToList();

        return BuscarCuentasResponse.CreateInstance(cuentas);
    }
}
