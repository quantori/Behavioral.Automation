using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using Behavioral.Automation.Services.Mapping;
using Behavioral.Automation.Services.Mapping.Contract;
using BoDi;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.DemoBindings
{
    [Binding]
    public class Bootstrapper
    {
        private RemoteWebDriver _driver;
        private readonly IObjectContainer _objectContainer;
        private readonly ITestRunner _runner;
        private readonly ScenarioContext _scenarioContext;
        private static readonly BrowserRunner BrowserRunner = new BrowserRunner();

        public Bootstrapper(IObjectContainer objectContainer, ITestRunner runner, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _runner = runner;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario(Order = 0)]
        public void Bootstrap()
        {
            _driver = BrowserRunner.OpenChrome();
            _objectContainer.RegisterInstanceAs(_driver);
            Assert.SetRunner(_runner);
            _objectContainer.RegisterTypeAs<UserInterfaceBuilder, IUserInterfaceBuilder>();
            _objectContainer.RegisterTypeAs<ScenarioExecutionConsumer, IScenarioExecutionConsumer>();
            _objectContainer.RegisterTypeAs<DriverService, IDriverService>();
            _objectContainer.RegisterTypeAs<ElementSelectionService, IElementSelectionService>();
            _objectContainer.RegisterTypeAs<VirtualizedElementsSelectionService, IVirtualizedElementsSelectionService>();
            _objectContainer.RegisterTypeAs<AutomationIdProvider, IAutomationIdProvider>();
            _objectContainer.RegisterTypeAs<ScopeMarkupStorageContainer, IScopeMarkupStorageContainer>();
            _objectContainer.RegisterTypeAs<ScopeMarkupMapper, IScopeMarkupMapper>();
            _objectContainer.RegisterTypeAs<UriToPageScopeMapper, IUriToPageScopeMapper>();
            _objectContainer.RegisterTypeAs<ScopeContextManager, IScopeContextManager>();
            _objectContainer.RegisterTypeAs<ScopeContextRuntime, IScopeContextRuntime>();

            var builder = _objectContainer.Resolve<IUserInterfaceBuilder>();
            builder.Build();

            Assert.SetConsumer(_objectContainer.Resolve<IScenarioExecutionConsumer>());
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driverService = _objectContainer.Resolve<IDriverService>();

            if (_scenarioContext.TestError != null)
            {
                driverService.MakeScreenShot();
            }
            var driver = _objectContainer.Resolve<RemoteWebDriver>();
            driver.Dispose();
        }
    }
}
