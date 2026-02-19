using System.Net.Http.Json;
using Temporalio.Activities;
using TemporalGreeting.Contracts;

namespace TemporalGreeting.Worker;

public class GreetingActivities
{
    private readonly HttpClient _httpClient;
    private readonly string _restServiceUrl;

    public GreetingActivities(HttpClient httpClient, string restServiceUrl)
    {
        _httpClient = httpClient;
        _restServiceUrl = restServiceUrl;
    }

    [Activity]
    public async Task<GreetingOutput> GreetAsync(GreetingInput input)
    {
        var activityId = ActivityExecutionContext.Current.Info.ActivityId;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Activity GreetAsync started — Name: {input.Name}, ActivityId: {activityId}");

        var cancellationToken = ActivityExecutionContext.Current.CancellationToken;
        var url = $"{_restServiceUrl}/greet";
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Activity calling REST service at {url}");
        var response = await _httpClient.PostAsJsonAsync(
            url,
            new { input.Name },
            cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<GreetingOutput>(cancellationToken);
        var output = result ?? throw new InvalidOperationException("REST service returned null response.");
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Activity completed — {output.Message}");
        return output;
    }
}
