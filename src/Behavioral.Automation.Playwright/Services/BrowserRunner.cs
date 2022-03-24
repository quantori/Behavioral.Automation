using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Behavioral.Automation.Services;
using BoDi;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Services
{
    public class BrowserRunner
    {
        private IBrowser _browser;
        
        public async Task OpenChrome(IObjectContainer container)
        {
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = ConfigServiceBase.BrowserParameters.Contains("--headless"),
                Channel = "chrome",
                SlowMo = 500
            });
            var resolution = ParseWindowSize(ConfigServiceBase.BrowserParameters);
            var page = await _browser.NewPageAsync();
            await page.SetViewportSizeAsync(resolution.FirstOrDefault(), resolution.LastOrDefault());
            await page.GotoAsync(ConfigServiceBase.BaseUrl);
            container.RegisterInstanceAs(playwright);
            container.RegisterInstanceAs(_browser);
            container.RegisterInstanceAs(page);
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