using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using BoDi;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.DemoBindings
{
    [Binding]
    public class Bootstrapper
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ITestRunner _runner;
        private readonly DemoTestServicesBuilder _servicesBuilder;
        private readonly BrowserContext _browserContext;

        public Bootstrapper(IObjectContainer objectContainer, ITestRunner runner, BrowserContext browserContext)
        {
            _objectContainer = objectContainer;
            _runner = runner;
            _browserContext = browserContext;
            _servicesBuilder = new DemoTestServicesBuilder(objectContainer, new TestServicesBuilder(_objectContainer));
        }

        [AfterScenario]
        public void CloseBrowser()
        {
            _browserContext.CloseBrowser();
        }

        [BeforeScenario(Order = 0)]
        public void Bootstrap()
        {
            Assert.SetRunner(_runner);
            _objectContainer.RegisterTypeAs<UserInterfaceBuilder, IUserInterfaceBuilder>();
            _servicesBuilder.Build();
            Assert.SetConsumer(_objectContainer.Resolve<IScenarioExecutionConsumer>());
            _browserContext.OpenChrome();
        }
    }
}
