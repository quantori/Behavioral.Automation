using System.Runtime.ConstrainedExecution;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using BoDi;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Implementation.Selenium.Services;

[Binding]
public class Hooks
{
    private readonly ObjectContainer _objectContainer;

    public Hooks(ObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }
    
    // Configuration of DI and Factories should be done with order 0
    [BeforeScenario(Order = 0)]
    public void ConfigureUiImplementations()
    {
        _objectContainer.RegisterTypeAs<WebElementStorageService, IWebElementStorageService>();
    }
}