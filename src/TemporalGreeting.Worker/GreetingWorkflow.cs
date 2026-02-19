using Microsoft.Extensions.Logging;
using Temporalio.Workflows;
using TemporalGreeting.Contracts;

namespace TemporalGreeting.Worker;

[Workflow]
public class GreetingWorkflow
{
    [WorkflowRun]
    public async Task<GreetingOutput> RunAsync(GreetingInput input)
    {
        Workflow.Logger.LogInformation("Workflow started — Name: {Name}, WorkflowId: {WorkflowId}",
            input.Name, Workflow.Info.WorkflowId);

        var result = await Workflow.ExecuteActivityAsync(
            (GreetingActivities act) => act.GreetAsync(input),
            new ActivityOptions { StartToCloseTimeout = TimeSpan.FromSeconds(30) });

        Workflow.Logger.LogInformation("Workflow completed — {Message}", result.Message);
        return result;
    }
}
