using Temporalio.Client;
using TemporalGreeting.Contracts;
using TemporalGreeting.Worker;

namespace TemporalGreeting.Client;

public class TemporalGreetingClient : IGreetingClient
{
    private readonly AppSettings _settings;

    public TemporalGreetingClient(AppSettings settings)
    {
        _settings = settings;
    }

    public async Task<GreetingOutput> GreetAsync(GreetingInput input)
    {
        var client = await TemporalConnectionFactory.ConnectAsync();

        return await client.ExecuteWorkflowAsync(
            (GreetingWorkflow wf) => wf.RunAsync(input),
            new WorkflowOptions
            {
                Id = $"greeting-{Guid.NewGuid()}",
                TaskQueue = _settings.Temporal.TaskQueue,
            });
    }
}
