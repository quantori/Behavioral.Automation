using Microsoft.Extensions.Configuration;

namespace Behavioral.Automation.Configs;

public static class ConfigManager
{
    private static IConfiguration Configuration { get; }

    static ConfigManager()
    {
        var env = Environment.GetEnvironmentVariable("STAND") ?? "";

        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("AutomationConfig.json", true, true);

        if (!string.IsNullOrWhiteSpace(env))
        {
            configurationBuilder.AddJsonFile($"AutomationConfig.{env.ToLower()}.json", true, true);
        }

        Configuration = configurationBuilder.AddEnvironmentVariables().Build();
    }

    public static T GetConfig<T>()
    {
        if (Configuration is null) throw new ArgumentException("Please, init configuration");

        var config = Configuration.Get<T>();
        return config;
    }

    public static string GetConfig(string configKey)
    {
        return Configuration.GetSection(configKey).Value;
    }
}