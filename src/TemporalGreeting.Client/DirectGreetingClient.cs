using System.Net.Http.Json;
using TemporalGreeting.Contracts;

namespace TemporalGreeting.Client;

public class DirectGreetingClient : IGreetingClient
{
    private readonly string _restServiceUrl;

    public DirectGreetingClient(string restServiceUrl)
    {
        _restServiceUrl = restServiceUrl;
    }

    public async Task<GreetingOutput> GreetAsync(GreetingInput input)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.PostAsJsonAsync(
            $"{_restServiceUrl}/greet",
            new { input.Name });
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<GreetingOutput>();
        return result ?? throw new InvalidOperationException("REST service returned null response.");
    }
}
