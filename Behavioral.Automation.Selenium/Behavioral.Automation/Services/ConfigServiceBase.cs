using System;
using Behavioral.Automation.Configs;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Access config properties
    /// </summary>
    [UsedImplicitly]
    public static class ConfigServiceBase
    {
        public static readonly IConfigurationRoot ConfigRoot =
            new ConfigurationBuilder()
                .AddJsonFile("AutomationConfig.json")
                .AddEnvironmentVariables()
                .Build();
        public static string UnhandledPromptBehavior => ConfigManager.GetConfig<Config>().UnhandledPromptBehavior;
        
        public static string BaseUrl => ConfigManager.GetConfig<Config>().BaseUrl;

        public static string BrowserParameters => ConfigManager.GetConfig<Config>().BrowserParameters;

        public static string SearchAttribute => ConfigManager.GetConfig<Config>().SearchAttribute;

        public static bool AcceptInsecureCertificates => ConfigManager.GetConfig<Config>().AcceptInsecureCertificates;

        public static bool AccessClipboard => ConfigManager.GetConfig<Config>().AccessClipboard;

        public static string DownloadPath => ConfigManager.GetConfig<Config>().DownloadPath;

        public static string BrowserBinaryLocation => ConfigManager.GetConfig<Config>().BrowserBinaryLocation;
        
        public static int? AssertAttempts => ConfigManager.GetConfig<Config>().AssertAttempts;

    }
}
