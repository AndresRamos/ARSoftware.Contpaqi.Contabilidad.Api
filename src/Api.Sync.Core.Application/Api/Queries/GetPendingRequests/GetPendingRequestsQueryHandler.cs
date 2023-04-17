﻿using Api.Core.Domain.Common;
using Api.Sync.Core.Application.Api.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Api.Queries.GetPendingRequests;

public sealed class GetPendingRequestsQueryHandler : IRequestHandler<GetPendingRequestsQuery, IEnumerable<ApiRequestBase>>
{
    private readonly IContpaqiComercialApiService _contpaqiComercialApiService;

    public GetPendingRequestsQueryHandler(IContpaqiComercialApiService contpaqiComercialApiService)
    {
        _contpaqiComercialApiService = contpaqiComercialApiService;
    }

    public async Task<IEnumerable<ApiRequestBase>> Handle(GetPendingRequestsQuery request, CancellationToken cancellationToken)
    {
        return (await _contpaqiComercialApiService.GetPendingRequestsAsync(cancellationToken)).ToList();
    }
}