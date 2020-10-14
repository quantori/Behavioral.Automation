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
        private static DemoTestServicesBuilder _servicesBuilder;
        private static readonly BrowserRunner BrowserRunner = new BrowserRunner();

        public Bootstrapper(IObjectContainer objectContainer, ITestRunner runner)
        {
            _objectContainer = objectContainer;
            _runner = runner;
            _servicesBuilder = new DemoTestServicesBuilder(objectContainer, new TestServicesBuilder(_objectContainer));
        }

        [BeforeTestRun]
        public static void OpenBrowser()
        {
            BrowserRunner.OpenChrome();
        }

        [AfterTestRun]
        public static void CloseBrowser()
        {
            BrowserRunner.CloseBrowser();
        }

        [BeforeScenario(Order = 0)]
        public void Bootstrap()
        {
            Assert.SetRunner(_runner);
            _objectContainer.RegisterTypeAs<UserInterfaceBuilder, IUserInterfaceBuilder>();
            _servicesBuilder.Build();
            Assert.SetConsumer(_objectContainer.Resolve<IScenarioExecutionConsumer>());
        }
    }
}
