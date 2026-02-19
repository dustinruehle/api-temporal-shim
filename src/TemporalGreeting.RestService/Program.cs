var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/greet", (GreetRequest request) =>
{
    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] POST /greet — Name: {request.Name}");
    var message = $"Hello, {request.Name}! Greetings from the REST service.";
    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Response — {message}");
    return Results.Ok(new GreetResponse(message));
});

app.Run();

record GreetRequest(string Name);

record GreetResponse(string Message);
