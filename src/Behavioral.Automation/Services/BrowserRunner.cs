using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Behavioral.Automation.Services
{
    public class BrowserRunner
    {
        public RemoteWebDriver Driver;
        
        public void OpenBrowser([NotNull] RemoteWebDriver driver)
        {
            Driver = driver;
        }

        public void CloseBrowser()
        {
            Driver.Dispose();
        }

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
            driver.ExecuteChromeCommand("Page.setDownloadBehavior", param);
            OpenBrowser(driver);
        }

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