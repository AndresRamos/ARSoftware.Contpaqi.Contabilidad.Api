using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.DiariosEspeciales;

public sealed class BuscarDiariosEspecialesRequestHandler : IRequestHandler<BuscarDiariosEspecialesRequest, ApiResponseBase>
{
    private readonly IDiarioEspecialRepository _diarioEspecialRepository;

    public BuscarDiariosEspecialesRequestHandler(IDiarioEspecialRepository diarioEspecialRepository)
    {
        _diarioEspecialRepository = diarioEspecialRepository;
    }

    public async Task<ApiResponseBase> Handle(BuscarDiariosEspecialesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<DiarioEspecial> diarios =
                (await _diarioEspecialRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarDiariosEspecialesResponse, BuscarDiariosEspecialesResponseModel>(request.Id,
                new BuscarDiariosEspecialesResponseModel { DiariosEspeciales = diarios });
        }
        catch (Exception e)
        {
            return ApiResponseFactory.CreateFailed<BuscarDiariosEspecialesResponse>(request.Id, e.Message);
        }
    }
}
