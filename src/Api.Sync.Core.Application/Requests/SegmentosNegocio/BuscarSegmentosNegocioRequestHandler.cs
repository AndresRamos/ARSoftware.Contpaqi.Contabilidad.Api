using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.SegmentosNegocio;

public sealed class BuscarSegmentosNegocioRequestHandler : IRequestHandler<BuscarSegmentosNegocioRequest, ApiResponseBase>
{
    private readonly ISegmentoNegocioRepository _segmentoNegocioRepository;

    public BuscarSegmentosNegocioRequestHandler(ISegmentoNegocioRepository segmentoNegocioRepository)
    {
        _segmentoNegocioRepository = segmentoNegocioRepository;
    }

    public async Task<ApiResponseBase> Handle(BuscarSegmentosNegocioRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<SegmentoNegocio> segmentosNegocio =
                (await _segmentoNegocioRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarSegmentosNegocioResponse, BuscarSegmentosNegocioResponseModel>(request.Id,
                new BuscarSegmentosNegocioResponseModel { SegmentosNegocio = segmentosNegocio });
        }
        catch (Exception e)
        {
            return ApiResponseFactory.CreateFailed<BuscarSegmentosNegocioResponse>(request.Id, e.Message);
        }
    }
}
