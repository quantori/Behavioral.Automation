using System.Threading.Tasks;
using Behavioral.Automation;
using Behavioral.Automation.Services;
using BoDi;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using NUnit.Framework;
using IDriverService = Behavioral.Automation.Playwright.Services.IDriverService;

namespace Behavioral.Automation.Playwright
{
    [Binding]
    public class Bootstrapper
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IObjectContainer _objectContainer;
        private static TestServicesBuilder _testServicesBuilder;

        public Bootstrapper(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
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

        [AfterScenario]
        public void SaveScreenshotAndLog()
        {
            var driverService = _objectContainer.Resolve<IDriverService>();
            if (_scenarioContext.TestError != null)
            {
                var screenShotPath = driverService.MakeScreenShot();
                TestContext.AddTestAttachment(screenShotPath);
            }
        }
    }
}