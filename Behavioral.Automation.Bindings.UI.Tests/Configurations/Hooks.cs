using Behavioral.Automation.Bindings.UI.Abstractions;
using BoDi;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings.UI.Tests.Configurations;

[Binding]
public class Configuration
{
    private readonly IObjectContainer _objectContainer;

    public Configuration(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    public void ResolveDependencyInjectionInterfaces()
    {
        _objectContainer.RegisterTypeAs<WebElementStorageService, IWebElementStorageService>();
    }
}