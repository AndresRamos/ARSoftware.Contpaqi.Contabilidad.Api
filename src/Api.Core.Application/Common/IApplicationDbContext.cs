using Api.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Common;

public interface IApplicationDbContext
{
    DbSet<Request> Requests { get; }
    DbSet<Response> Responses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
