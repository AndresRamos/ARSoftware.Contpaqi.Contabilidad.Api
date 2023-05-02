using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.DiariosEspeciales;

public sealed class BuscarDiariosEspecialesRequestHandler : IRequestHandler<BuscarDiariosEspecialesRequest, BuscarDiariosEspecialesResponse>
{
    private readonly IDiarioEspecialRepository _diarioEspecialRepository;

    public BuscarDiariosEspecialesRequestHandler(IDiarioEspecialRepository diarioEspecialRepository)
    {
        _diarioEspecialRepository = diarioEspecialRepository;
    }

    public async Task<BuscarDiariosEspecialesResponse> Handle(BuscarDiariosEspecialesRequest request, CancellationToken cancellationToken)
    {
        List<DiarioEspecial> diarios =
            (await _diarioEspecialRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

        return BuscarDiariosEspecialesResponse.CreateInstance(diarios);
    }
}
