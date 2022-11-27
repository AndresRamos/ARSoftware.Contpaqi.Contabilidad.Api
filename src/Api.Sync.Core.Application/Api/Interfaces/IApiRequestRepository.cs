using Api.SharedKernel.Common;

namespace Api.Sync.Core.Application.Api.Interfaces;

public interface IApiRequestRepository
{
    Task<IEnumerable<ApiRequestBase>> GetPendingRequestsAsync(CancellationToken cancellationToken);
    Task SetResponseAsync(Guid requestId, ApiResponseBase response, CancellationToken cancellationToken);
}
