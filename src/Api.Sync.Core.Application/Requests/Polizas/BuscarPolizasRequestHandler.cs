using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Polizas;

public sealed class BuscarPolizasRequestHandler : IRequestHandler<BuscarPolizasRequest, ApiResponse>
{
    private readonly IPolizaRepository _polizaRepository;

    public BuscarPolizasRequestHandler(IPolizaRepository polizaRepository)
    {
        _polizaRepository = polizaRepository;
    }

    public async Task<ApiResponse> Handle(BuscarPolizasRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Poliza> polizas = (await _polizaRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
                .ToList();

            return ApiResponse.CreateSuccessfull(new BuscarPolizasResponse
            {
                Model = new BuscarPolizasResponseModel { Polizas = polizas }
            });
        }
        catch (Exception e)
        {
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
