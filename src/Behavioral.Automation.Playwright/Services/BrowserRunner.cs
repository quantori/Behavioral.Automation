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
        private readonly IObjectContainer _objectContainer;
        private static TestServicesBuilder _testServicesBuilder;

        public BrowserRunner(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _objectContainer = objectContainer;
            _testServicesBuilder = new TestServicesBuilder(_objectContainer);
        }

        [BeforeScenario]
        public async Task OpenChrome(IObjectContainer container)
        {
            _testServicesBuilder.Build();
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = ConfigServiceBase.BrowserParameters.Contains("--headless"),
                Channel = "chrome",
                SlowMo = 500
            });
            var page = await browser.NewPageAsync();
            await page.GotoAsync(ConfigServiceBase.BaseUrl);
            container.RegisterInstanceAs(playwright);
            container.RegisterInstanceAs(browser);
            container.RegisterInstanceAs(page);
        }
    }
}