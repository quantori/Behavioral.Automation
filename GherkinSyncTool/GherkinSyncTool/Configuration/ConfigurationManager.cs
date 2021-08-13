using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GherkinSyncTool.Configuration
{
    public static class ConfigurationManager
    {
        private static Config _configurationModel;

        public static Config GetConfiguration()
        {
            return _configurationModel??=GetIConfiguration().Get<Config>();
        }
        private static IConfiguration GetIConfiguration(string[] args = null)
        {
            args ??= Array.Empty<string>();
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }
    }
}