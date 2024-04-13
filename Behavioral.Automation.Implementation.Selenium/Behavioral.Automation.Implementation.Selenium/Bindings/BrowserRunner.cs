using System.Diagnostics.CodeAnalysis;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Implementation.Selenium.Configs;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Behavioral.Automation.Implementation.Selenium.Bindings;

public class BrowserRunner
{
    public IWebDriver Driver;

    /// <summary>
    /// Driver initialization
    /// </summary>
    /// <param name="driver">IWebDriver object</param>
    public void OpenBrowser([NotNull] IWebDriver driver)
    {
        Driver = driver;
    }


    /// <summary>
    /// Driver disposal
    /// </summary>
    public void CloseBrowser()
    {
        Driver.Dispose();
    }

    /// <summary>
    /// Open and configure Google Chrome browser
    /// </summary>
    /// <param name="options">Browser parameters</param>
    public void OpenChrome(ChromeOptions options = null)
    {
        // new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

        var downloadPath = ConfigManager.GetConfig<Config>().DownloadPath ?? Environment.CurrentDirectory;

        if (options == null)
        {
            options = new ChromeOptions();
            options.AddArguments(new List<string>(
                ConfigManager.GetConfig<Config>().BrowserParameters.Split(" ", StringSplitOptions.RemoveEmptyEntries)));
            if (ConfigManager.GetConfig<Config>().BrowserParameters.Contains("headless"))
            {
                ConfigureChromeHeadlessDownload(options, downloadPath);
            }

            if (ConfigManager.GetConfig<Config>().AccessClipboard)
            {
                ConfigureClipboardAccess(options);
            }

            options.AddUserProfilePreference("intl.accept_languages", "en,en_US");
            options.AcceptInsecureCertificates = ConfigManager.GetConfig<Config>().AcceptInsecureCertificates;
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            if (!string.IsNullOrWhiteSpace(ConfigManager.GetConfig<Config>().BrowserBinaryLocation))
            {
                options.BinaryLocation =
                    Environment.ExpandEnvironmentVariables(ConfigManager.GetConfig<Config>().BrowserBinaryLocation);
            }

            if (!string.IsNullOrWhiteSpace(ConfigManager.GetConfig<Config>().UnhandledPromptBehavior))
            {
                options.UnhandledPromptBehavior = ConfigManager.GetConfig<Config>().UnhandledPromptBehavior switch
                {
                    "Accept" => UnhandledPromptBehavior.Accept,
                    "Dismiss" => UnhandledPromptBehavior.Dismiss,
                    "Ignore" => UnhandledPromptBehavior.Ignore,
                    _ => options.UnhandledPromptBehavior
                };
            }
        }

        var driver = new ChromeDriver(options);
        var param = new Dictionary<string, object>
        {
            {"behavior", "allow"},
            {"downloadPath", downloadPath}
        };
        driver.ExecuteCdpCommand("Page.setDownloadBehavior", param);
        OpenBrowser(driver);
    }

    /// <summary>
    /// Configure Google Chrome downloads to work correctly in headless mode
    /// </summary>
    /// <param name="options">Chrome configuration parameters</param>
    /// <param name="downloadPath">Directory to download files to</param>
    private void ConfigureChromeHeadlessDownload(ChromeOptions options, string downloadPath)
    {
        options.AddUserProfilePreference("download.prompt_for_download", "false");
        options.AddUserProfilePreference("download.directory_upgrade", "true");
        options.AddUserProfilePreference("download.default_directory", downloadPath);
    }

    private void ConfigureClipboardAccess(ChromeOptions options)
    {
        var clipboardExceptionSettings = new Dictionary<string, object>
        {
            {
                ConfigManager.GetConfig<Config>().BaseUrl,
                new Dictionary<string, long>
                {
                    {"last_modified", DateTimeOffset.Now.ToUnixTimeMilliseconds()},
                    {"setting", 1}
                }
            }
        };
        options.AddUserProfilePreference("profile.content_settings.exceptions.clipboard", clipboardExceptionSettings);
    }
}