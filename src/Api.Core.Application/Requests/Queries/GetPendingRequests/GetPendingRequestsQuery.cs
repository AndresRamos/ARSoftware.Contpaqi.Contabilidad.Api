using Api.SharedKernel.Models;
using MediatR;

namespace Api.Core.Application.Requests.Queries.GetPendingRequests;

public class GetPendingRequestsQuery : IRequest<IEnumerable<Request>>
{
}
