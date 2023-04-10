using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Cuentas;

public sealed record BuscarCuentasRequestHandler : IRequestHandler<BuscarCuentasRequest, ApiResponseBase>
{
    private readonly ICuentaRepository _cuentaRepository;

    public BuscarCuentasRequestHandler(ICuentaRepository cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    public async Task<ApiResponseBase> Handle(BuscarCuentasRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Cuenta> cuentas = (await _cuentaRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
                .ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarCuentasResponse, BuscarCuentasResponseModel>(request.Id,
                new BuscarCuentasResponseModel { Cuentas = cuentas });
        }
        catch (Exception e)
        {
            return ApiResponseFactory.CreateFailed<BuscarCuentasResponse>(request.Id, e.Message);
        }
    }
}
