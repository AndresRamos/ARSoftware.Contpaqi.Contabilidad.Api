using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.TiposPoliza;

public sealed class BuscarTiposPolizaRequestHandler : IRequestHandler<BuscarTiposPolizaRequest, ApiResponse>
{
    private readonly ITipoPolizaRepository _tipoPolizaRepository;

    public BuscarTiposPolizaRequestHandler(ITipoPolizaRepository tipoPolizaRepository)
    {
        _tipoPolizaRepository = tipoPolizaRepository;
    }

    public async Task<ApiResponse> Handle(BuscarTiposPolizaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<TipoPoliza> tiposPoliza =
                (await _tipoPolizaRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

            return ApiResponse.CreateSuccessfull(new BuscarTiposPolizaResponse
            {
                Model = new BuscarTiposPolizaResponseModel { TiposPoliza = tiposPoliza }
            });
        }
        catch (Exception e)
        {
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
