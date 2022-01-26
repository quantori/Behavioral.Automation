using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using Behavioral.Automation.Template.Services;
using BoDi;
using TechTalk.SpecFlow;
using Behavioral.Automation;

namespace Behavioral.Automation.Template.Bindings
{
    [Binding]
    public class Bootstrapper
    {
        private readonly IObjectContainer _objectContainer;
        private readonly ITestRunner _runner;
        private readonly DemoTestServicesBuilder _servicesBuilder;
        private readonly BrowserRunner _browserRunner;

        public Bootstrapper(IObjectContainer objectContainer, ITestRunner runner, BrowserRunner browserRunner)
        {
            _objectContainer = objectContainer;
            _runner = runner;
            _browserRunner = browserRunner;
            _servicesBuilder = new DemoTestServicesBuilder(objectContainer, new TestServicesBuilder(_objectContainer));
        }

        [AfterScenario]
        public void CloseBrowser()
        {
            _browserRunner.CloseBrowser();
        }

        [BeforeScenario(Order = 0)]
        public void Bootstrap()
        {
            Assert.SetRunner(_runner);
            _objectContainer.RegisterTypeAs<UserInterfaceBuilder, IUserInterfaceBuilder>();
            _servicesBuilder.Build();
            _browserRunner.OpenChrome();
        }
    }
}
