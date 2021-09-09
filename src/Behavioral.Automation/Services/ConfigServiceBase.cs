using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Behavioral.Automation.Services
{
    [UsedImplicitly]
    public static class ConfigServiceBase
    {
        public static readonly IConfigurationRoot ConfigRoot =
            new ConfigurationBuilder()
                .AddJsonFile("AutomationConfig.json")
                .AddEnvironmentVariables()
                .Build();

        private const string BaseUrlString = "BASE_URL";

        private const string BrowserParametersString = "BROWSER_PARAMS";

        private const string DownloadPathString = "DOWNLOAD_PATH";

        private static readonly string AcceptInsecureCertificatesString = "ACCEPT_INSECURE_CERTIFICATES";

        private const string SearchAttributeString = "SEARCH_ATTRIBUTE";

        private const string AccessClipboardString = "ACCESS_CLIPBOARD";
        
        private const string BrowserBinaryLocationString = "BROWSER_BINARY_LOCATION";

        public static string BaseUrl => ConfigRoot[BaseUrlString];

        public static string BrowserParameters => ConfigRoot[BrowserParametersString];

        public static string SearchAttribute => ConfigRoot[SearchAttributeString];

        public static bool AcceptInsecureCertificates => ConfigRoot.GetValue<bool>(AcceptInsecureCertificatesString);

        public static bool AccessClipboard => ConfigRoot.GetValue<bool>(AccessClipboardString);

        public static string DownloadPath
        {
            get
            {
                if (ConfigRoot[DownloadPathString] != string.Empty)
                {
                    return ConfigRoot[DownloadPathString];
                }

                return AppContext.BaseDirectory;
            }
        }
        
        public static string BrowserBinaryLocation
        {
            get => ConfigRoot[BrowserBinaryLocationString];
            set => ConfigRoot[BrowserBinaryLocationString] = value;
        }
    }
}
