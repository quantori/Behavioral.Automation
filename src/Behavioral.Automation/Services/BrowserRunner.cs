using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Methods for browser configuration
    /// </summary>
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
            var downloadPath = ConfigServiceBase.DownloadPath ?? Environment.CurrentDirectory;

            if (options == null)
            {
                options = new ChromeOptions();
                options.AddArguments(new List<string>(
                   ConfigServiceBase.BrowserParameters.Split(" ", StringSplitOptions.RemoveEmptyEntries)));
                if (ConfigServiceBase.BrowserParameters.Contains("headless"))
                {
                    ConfigureChromeHeadlessDownload(options, downloadPath);
                }
                if (ConfigServiceBase.AccessClipboard)
                {
                    ConfigureClipboardAccess(options);
                }
                options.AddUserProfilePreference("intl.accept_languages", "en,en_US");
                options.AcceptInsecureCertificates = ConfigServiceBase.AcceptInsecureCertificates;
                options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                if (!string.IsNullOrWhiteSpace(ConfigServiceBase.BrowserBinaryLocation))
                {
                    options.BinaryLocation = Environment.ExpandEnvironmentVariables(ConfigServiceBase.BrowserBinaryLocation);
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
            var clipboardExceptionSettings = new Dictionary<string, object> {
                {ConfigServiceBase.BaseUrl,
                    new Dictionary<string, long> {
                        {"last_modified", DateTimeOffset.Now.ToUnixTimeMilliseconds()},
                        {"setting", 1}
                    }
                }
            };
            options.AddUserProfilePreference("profile.content_settings.exceptions.clipboard", clipboardExceptionSettings);
        }
    }
}