using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.TiposPoliza;

public sealed class BuscarTiposPolizaRequestHandler : IRequestHandler<BuscarTiposPolizaRequest, ApiResponseBase>
{
    private readonly ITipoPolizaRepository _tipoPolizaRepository;

    public BuscarTiposPolizaRequestHandler(ITipoPolizaRepository tipoPolizaRepository)
    {
        _tipoPolizaRepository = tipoPolizaRepository;
    }

    public async Task<ApiResponseBase> Handle(BuscarTiposPolizaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<TipoPoliza> tiposPoliza =
                (await _tipoPolizaRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarTiposPolizaResponse, BuscarTiposPolizaResponseModel>(request.Id,
                new BuscarTiposPolizaResponseModel { TiposPoliza = tiposPoliza });
        }
        catch (Exception e)
        {
            return ApiResponseFactory.CreateFailed<BuscarTiposPolizaResponse>(request.Id, e.Message);
        }
    }
}
