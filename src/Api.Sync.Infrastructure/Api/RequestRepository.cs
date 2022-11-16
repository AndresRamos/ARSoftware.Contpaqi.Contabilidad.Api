using System.Text;
using System.Text.Json;
using Api.SharedKernel.Models;
using Api.Sync.Core.Application.Api.Interfaces;

namespace Api.Sync.Infrastructure.Api;

public sealed class RequestRepository : IRequestRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RequestRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<Request>> GetPendingRequestsAsync(CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");

        Stream stream = await httpClient.GetStreamAsync("requests/pending", cancellationToken);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        return await JsonSerializer.DeserializeAsync<IEnumerable<Request>>(stream, options, cancellationToken);
    }

    public async Task SetResponseAsync(Guid requestId, Response response, CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ApiClient");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var command = new { id = requestId, response };

        string json = JsonSerializer.Serialize(command);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync($"requests/{requestId}/response", data, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();
    }
}
