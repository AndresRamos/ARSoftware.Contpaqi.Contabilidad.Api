using Api.SharedKernel.Models;
using MediatR;

namespace Api.Core.Application.Requests.Queries.GetRequestById;

public class GetRequestByIdQuery : IRequest<Request?>
{
    public GetRequestByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
