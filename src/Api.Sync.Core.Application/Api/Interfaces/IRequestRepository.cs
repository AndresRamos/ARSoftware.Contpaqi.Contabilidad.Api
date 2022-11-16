using Api.SharedKernel.Models;

namespace Api.Sync.Core.Application.Api.Interfaces;

public interface IRequestRepository
{
    Task<IEnumerable<Request>> GetPendingRequestsAsync(CancellationToken cancellationToken);
    Task SetResponseAsync(Guid requestId, Response response, CancellationToken cancellationToken);
}
