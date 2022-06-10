using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Services;

public class BrowserRunner
    {
        public async Task<IPage> LaunchBrowser()
        {
            using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            return page;
        }
    }