using Microsoft.Extensions.Configuration;
using System;

namespace Behavioral.Automation.Configs;

public class Config
{
    [ConfigurationKeyName("BASE_URL")] public string BaseUrl { get; set; }

    [ConfigurationKeyName("BROWSER_PARAMS")]
    public string BrowserParameters { get; set; }

    [ConfigurationKeyName("DOWNLOAD_PATH")]
    public string DownloadPath { get; set; } = AppContext.BaseDirectory;

    [ConfigurationKeyName("ACCEPT_INSECURE_CERTIFICATES")]
    public bool AcceptInsecureCertificates { get; set; } = true;

    [ConfigurationKeyName("SEARCH_ATTRIBUTE")]
    public string SearchAttribute { get; set; }

    [ConfigurationKeyName("ACCESS_CLIPBOARD")]
    public bool AccessClipboard { get; set; } = false;

    [ConfigurationKeyName("BROWSER_BINARY_LOCATION")]
    public string BrowserBinaryLocation { get; set; }

    [ConfigurationKeyName("UNHANDLED_PROMPT_BEHAVIOR")]
    public string UnhandledPromptBehavior { get; set; }

    [ConfigurationKeyName("ASSERT_ATTEMPTS")]
    public int? AssertAttempts { get; set; } = 30;
}