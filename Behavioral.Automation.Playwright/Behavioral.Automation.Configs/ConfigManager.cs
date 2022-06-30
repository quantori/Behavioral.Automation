using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Behavioral.Automation.Configs;

public static class ConfigManager
{
    private static IConfiguration Configuration { get; }

    static ConfigManager()
    {
        var env = Environment.GetEnvironmentVariable("STAND");
        
        if (string.IsNullOrEmpty(env))
        {
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes();
            var configAttribute = attributes.FirstOrDefault(attribute => attribute.TypeId.Equals(typeof(AssemblyConfigurationAttribute)));
            var configEnv = (AssemblyConfigurationAttribute) configAttribute!;
            env = configEnv.Configuration;
        }

        Configuration = new ConfigurationBuilder()
            .AddJsonFile("AutomationConfig.json", true, true)
            .AddJsonFile($"AutomationConfig.{env.ToLower()}.json", true, true)
            .AddEnvironmentVariables()
            .Build();
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