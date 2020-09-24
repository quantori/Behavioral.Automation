using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Methods for browser configuration
    /// </summary>
    public class BrowserRunner
    {
        /// <summary>
        /// Driver initialization
        /// </summary>
        /// <param name="driver">RemoteWebDriverObject</param>
        public void OpenBrowser([NotNull] RemoteWebDriver driver)
        {
            DriverService.Driver = driver;
        }


        /// <summary>
        /// Driver disposal
        /// </summary>
        public void CloseBrowser()
        {
            DriverService.Driver.Dispose();
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
                options.AddUserProfilePreference("intl.accept_languages", "en,en_US");
                options.AcceptInsecureCertificates = ConfigServiceBase.AcceptInsecureCertificates;
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
    }
}