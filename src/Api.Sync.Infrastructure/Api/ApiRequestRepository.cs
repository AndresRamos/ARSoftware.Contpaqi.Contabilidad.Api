using System.Text;
using System.Text.Json;
using Api.SharedKernel.Common;
using Api.Sync.Core.Application.Api.Interfaces;

namespace Api.Sync.Infrastructure.Api;

public sealed class ApiRequestRepository : IApiRequestRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiRequestRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<ApiRequestBase>> GetPendingRequestsAsync(CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");

        Stream stream = await httpClient.GetStreamAsync("requests/pending", cancellationToken);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        return await JsonSerializer.DeserializeAsync<IEnumerable<ApiRequestBase>>(stream, options, cancellationToken) ??
               Enumerable.Empty<ApiRequestBase>();
    }

    public async Task SetResponseAsync(Guid requestId, ApiResponseBase response, CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        var command = new { id = requestId, response };

        string json = JsonSerializer.Serialize(command, options);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync($"requests/{requestId}/response", data, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();
    }
}
