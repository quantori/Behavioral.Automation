using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Behavioral.Automation.Services;
using BoDi;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Services
{
    [Binding]
    public class BrowserRunner
    {
        public IPage Page;
        private IBrowser _browser;

        public void OpenBrowser(IPage page)
        {
            Page = page;
        }
        
        public void OpenChrome(IObjectContainer container)
        {
            var playwrightTask = Microsoft.Playwright.Playwright.CreateAsync();
            playwrightTask.Wait();
            var playwright = playwrightTask.Result;
            var browserTask = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = ConfigServiceBase.BrowserParameters.Contains("--headless"),
                Channel = "chrome",
                SlowMo = 500
            });
            browserTask.Wait();
            _browser = browserTask.Result;
            var resolution = ParseWindowSize(ConfigServiceBase.BrowserParameters);
            var pageTask = _browser.NewPageAsync(new BrowserNewPageOptions
            {
                BaseURL = ConfigServiceBase.BaseUrl,
                ViewportSize = new ViewportSize
                {
                    Width = resolution.FirstOrDefault(),
                    Height = resolution.LastOrDefault()
                }
            });
            pageTask.Wait();
            var page = pageTask.Result;
            container.RegisterInstanceAs(playwright);
            container.RegisterInstanceAs(_browser);
            container.RegisterInstanceAs(page);
            OpenBrowser(page);
        }

        public void CloseBrowser()
        {
            _browser.CloseAsync();
        }

        private IEnumerable<int> ParseWindowSize(string config)
        {
            var regex = new Regex(@"(\d)*,(\d)*");
            return regex.Match(config).Value.Split(",").Select(i => int.Parse(i));
        }
    }
}