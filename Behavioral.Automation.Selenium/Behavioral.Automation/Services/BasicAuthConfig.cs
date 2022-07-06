using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Behavioral.Automation.Services
{
    [UsedImplicitly]
    public class BasicAuthConfig : IBasicAuthConfig
    {
        public static readonly IConfigurationRoot ConfigRoot =
            new ConfigurationBuilder()
                .AddJsonFile("AutomationConfig.json")
                .AddEnvironmentVariables()
                .Build();

        private const string HomeUrlString = "BAUTH_URL";

        private const string LoginString = "BAUTH_LOGIN";

        private const string PasswordString = "BAUTH_PASSWORD";

        private const string IgnoreAuthString = "BAUTH_IGNORE";

        public string HomeUrl => ConfigRoot[HomeUrlString];

        public string Login => ConfigRoot[LoginString];

        public string Password => ConfigRoot[PasswordString];

        public bool IgnoreAuth => Convert.ToBoolean(ConfigRoot[IgnoreAuthString]);
    }
}