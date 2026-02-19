using Microsoft.Extensions.Configuration;

namespace TemporalGreeting.Contracts;

public static class AppSettingsLoader
{
    public static AppSettings Load()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var settings = new AppSettings();
        configuration.Bind(settings);
        return settings;
    }
}
