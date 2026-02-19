using Temporalio.Client;
using Temporalio.Common.EnvConfig;

namespace TemporalGreeting.Contracts;

public static class TemporalConnectionFactory
{
    public static async Task<TemporalClient> ConnectAsync()
    {
        var options = ClientEnvConfig.LoadClientConnectOptions();
        return await TemporalClient.ConnectAsync(options);
    }
}
