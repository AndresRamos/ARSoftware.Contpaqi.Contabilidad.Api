using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiContabilidad.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Empresas;

public sealed class BuscarEmpresasRequestHandler : IRequestHandler<BuscarEmpresasRequest, ApiResponse>
{
    private readonly IEmpresaRepository _empresaRepository;

    public BuscarEmpresasRequestHandler(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task<ApiResponse> Handle(BuscarEmpresasRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Empresa> empresas = (await _empresaRepository.BuscarTodoAsync(request.Options, cancellationToken)).ToList();

            return ApiResponse.CreateSuccessfull(new BuscarEmpresasResponse
            {
                Model = new BuscarEmpresasResponseModel { Empresas = empresas }
            });
        }
        catch (Exception e)
        {
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
