using Api.SharedKernel.Common;
using MediatR;

namespace Api.Core.Application.Requests.Queries.GetRequestById;

public class GetRequestByIdQuery : IRequest<ApiRequestBase?>
{
    public GetRequestByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
