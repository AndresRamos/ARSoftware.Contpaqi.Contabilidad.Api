using Api.Sync.Core.Application.Api.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Api.Commands.SetResponse;

public sealed class SetResponseCommandHandler : IRequestHandler<SetResponseCommand>
{
    private readonly IApiRequestRepository _apiRequestRepository;

    public SetResponseCommandHandler(IApiRequestRepository apiRequestRepository)
    {
        _apiRequestRepository = apiRequestRepository;
    }

    public async Task<Unit> Handle(SetResponseCommand request, CancellationToken cancellationToken)
    {
        await _apiRequestRepository.SetResponseAsync(request.RequestId, request.Response, cancellationToken);

        return Unit.Value;
    }
}
