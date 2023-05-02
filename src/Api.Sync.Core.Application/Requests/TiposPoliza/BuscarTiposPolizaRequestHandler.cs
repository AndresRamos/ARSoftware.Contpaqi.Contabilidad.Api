using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.TiposPoliza;

public sealed class BuscarTiposPolizaRequestHandler : IRequestHandler<BuscarTiposPolizaRequest, BuscarTiposPolizaResponse>
{
    private readonly ITipoPolizaRepository _tipoPolizaRepository;

    public BuscarTiposPolizaRequestHandler(ITipoPolizaRepository tipoPolizaRepository)
    {
        _tipoPolizaRepository = tipoPolizaRepository;
    }

    public async Task<BuscarTiposPolizaResponse> Handle(BuscarTiposPolizaRequest request, CancellationToken cancellationToken)
    {
        List<TipoPoliza> tiposPoliza =
            (await _tipoPolizaRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

        return BuscarTiposPolizaResponse.CreateInstance(tiposPoliza);
    }
}
