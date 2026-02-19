using TemporalGreeting.Contracts;

namespace TemporalGreeting.Client;

public interface IGreetingClient
{
    Task<GreetingOutput> GreetAsync(GreetingInput input);
}
