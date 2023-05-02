using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Polizas;

public sealed class BuscarPolizasRequestHandler : IRequestHandler<BuscarPolizasRequest, BuscarPolizasResponse>
{
    private readonly IPolizaRepository _polizaRepository;

    public BuscarPolizasRequestHandler(IPolizaRepository polizaRepository)
    {
        _polizaRepository = polizaRepository;
    }

    public async Task<BuscarPolizasResponse> Handle(BuscarPolizasRequest request, CancellationToken cancellationToken)
    {
        List<Poliza> polizas = (await _polizaRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
            .ToList();

        return BuscarPolizasResponse.CreateInstance(polizas);
    }
}
