using TemporalGreeting.Client;
using TemporalGreeting.Contracts;

var settings = AppSettingsLoader.Load();

var mode = "temporal";
string? name = null;

for (var i = 0; i < args.Length; i++)
{
    if (args[i] == "--mode" && i + 1 < args.Length)
    {
        mode = args[++i].ToLowerInvariant();
    }
    else if (!args[i].StartsWith("--"))
    {
        name = args[i];
    }
}

if (string.IsNullOrWhiteSpace(name))
{
    Console.Error.WriteLine("Usage: dotnet run --project src/TemporalGreeting.Client -- [--mode temporal|direct] <name>");
    return 1;
}

IGreetingClient client = mode switch
{
    "temporal" => new TemporalGreetingClient(settings),
    "direct" => new DirectGreetingClient(settings.RestServiceUrl),
    _ => throw new ArgumentException($"Unknown mode: {mode}. Use 'temporal' or 'direct'."),
};

Console.WriteLine($"Mode: {mode}");
var result = await client.GreetAsync(new GreetingInput(name));
Console.WriteLine($"Result: {result.Message}");
return 0;
