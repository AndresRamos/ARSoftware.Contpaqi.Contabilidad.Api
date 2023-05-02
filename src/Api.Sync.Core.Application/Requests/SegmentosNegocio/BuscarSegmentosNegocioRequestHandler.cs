using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.SegmentosNegocio;

public sealed class BuscarSegmentosNegocioRequestHandler : IRequestHandler<BuscarSegmentosNegocioRequest, BuscarSegmentosNegocioResponse>
{
    private readonly ISegmentoNegocioRepository _segmentoNegocioRepository;

    public BuscarSegmentosNegocioRequestHandler(ISegmentoNegocioRepository segmentoNegocioRepository)
    {
        _segmentoNegocioRepository = segmentoNegocioRepository;
    }

    public async Task<BuscarSegmentosNegocioResponse> Handle(BuscarSegmentosNegocioRequest request, CancellationToken cancellationToken)
    {
        List<SegmentoNegocio> segmentosNegocio =
            (await _segmentoNegocioRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

        return BuscarSegmentosNegocioResponse.CreateInstance(segmentosNegocio);
    }
}
