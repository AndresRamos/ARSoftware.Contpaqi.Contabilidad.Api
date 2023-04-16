using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Empresas;

public sealed class BuscarEmpresasRequestHandler : IRequestHandler<BuscarEmpresasRequest, ApiResponseBase>
{
    private readonly IEmpresaRepository _empresaRepository;

    public BuscarEmpresasRequestHandler(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task<ApiResponseBase> Handle(BuscarEmpresasRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Empresa> empresas = (await _empresaRepository.BuscarTodoAsync(request.Options, cancellationToken)).ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarEmpresasResponse, BuscarEmpresasResponseModel>(request.Id,
                new BuscarEmpresasResponseModel { Empresas = empresas });
        }
        catch (Exception e)
        {
            return ApiResponseFactory.CreateFailed<BuscarEmpresasResponse>(request.Id, e.Message);
        }
    }
}
