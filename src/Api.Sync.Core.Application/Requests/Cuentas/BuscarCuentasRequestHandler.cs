using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Cuentas;

public sealed record BuscarCuentasRequestHandler : IRequestHandler<BuscarCuentasRequest, ApiResponse>
{
    private readonly ICuentaRepository _cuentaRepository;

    public BuscarCuentasRequestHandler(ICuentaRepository cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    public async Task<ApiResponse> Handle(BuscarCuentasRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Cuenta> cuentas = (await _cuentaRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
                .ToList();

            return ApiResponse.CreateSuccessfull(new BuscarCuentasResponse
            {
                Model = new BuscarCuentasResponseModel { Cuentas = cuentas }
            });
        }
        catch (Exception e)
        {
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
