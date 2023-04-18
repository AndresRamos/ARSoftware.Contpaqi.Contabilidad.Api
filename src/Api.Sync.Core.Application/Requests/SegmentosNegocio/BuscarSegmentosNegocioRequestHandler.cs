using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.SegmentosNegocio;

public sealed class BuscarSegmentosNegocioRequestHandler : IRequestHandler<BuscarSegmentosNegocioRequest, ApiResponse>
{
    private readonly ISegmentoNegocioRepository _segmentoNegocioRepository;

    public BuscarSegmentosNegocioRequestHandler(ISegmentoNegocioRepository segmentoNegocioRepository)
    {
        _segmentoNegocioRepository = segmentoNegocioRepository;
    }

    public async Task<ApiResponse> Handle(BuscarSegmentosNegocioRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<SegmentoNegocio> segmentosNegocio =
                (await _segmentoNegocioRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

            return ApiResponse.CreateSuccessfull(new BuscarSegmentosNegocioResponse
            {
                Model = new BuscarSegmentosNegocioResponseModel { SegmentosNegocio = segmentosNegocio }
            });
        }
        catch (Exception e)
        {
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
