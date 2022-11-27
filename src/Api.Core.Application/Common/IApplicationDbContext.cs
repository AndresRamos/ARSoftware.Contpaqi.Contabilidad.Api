using Api.SharedKernel.Common;
using Api.SharedKernel.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Core.Application.Common;

public interface IApplicationDbContext
{
    DbSet<ApiRequestBase> Requests { get; }
    DbSet<ApiResponseBase> Responses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
