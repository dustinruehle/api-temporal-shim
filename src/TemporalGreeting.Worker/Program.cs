using Temporalio.Worker;
using TemporalGreeting.Contracts;
using TemporalGreeting.Worker;

var settings = AppSettingsLoader.Load();
var client = await TemporalConnectionFactory.ConnectAsync();

using var worker = new TemporalWorker(
    client,
    new TemporalWorkerOptions(settings.Temporal.TaskQueue)
        .AddWorkflow<GreetingWorkflow>()
        .AddAllActivities(new GreetingActivities(new HttpClient(), settings.RestServiceUrl)));

Console.WriteLine($"Worker listening on task queue '{settings.Temporal.TaskQueue}' in namespace '{client.Options.Namespace}'...");
await worker.ExecuteAsync(new CancellationTokenSource().Token);
