using Api.SharedKernel.Common;
using MediatR;

namespace Api.Sync.Core.Application.Api.Queries.GetPendingRequests;

public sealed class GetPendingRequestsQuery : IRequest<IEnumerable<ApiRequestBase>>
{
}
