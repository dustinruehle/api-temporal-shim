namespace TemporalGreeting.Contracts;

public class TemporalSettings
{
    public string TaskQueue { get; set; } = "greeting-task-queue";
}

public class AppSettings
{
    public TemporalSettings Temporal { get; set; } = new();
    public string RestServiceUrl { get; set; } = "http://localhost:5050";
}
