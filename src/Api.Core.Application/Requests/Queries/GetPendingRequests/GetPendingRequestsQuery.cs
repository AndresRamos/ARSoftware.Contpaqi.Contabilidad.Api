using Api.SharedKernel.Common;
using MediatR;

namespace Api.Core.Application.Requests.Queries.GetPendingRequests;

public sealed class GetPendingRequestsQuery : IRequest<IEnumerable<ApiRequestBase>>
{
}
