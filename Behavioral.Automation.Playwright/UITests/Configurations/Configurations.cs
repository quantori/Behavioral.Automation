using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.WebElementsWrappers;
using BoDi;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Tests.Configurations;

[Binding]
public class Configurations
{
    private readonly ObjectContainer _objectContainer;
    
    /// <summary>
    /// According to our convention Configuration of DI and Factories should be done with order 0
    /// </summary>
    public Configurations(ObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeTestRun(Order = 0)]
    public static void ConfigureUiImplementations()
    {
        IWebElementStorageService.RegisterWebElementImplementationAs<ButtonElement, IButtonElement>();
    }
    
    /// <summary>
    /// According to our convention all interfaces registrations should go with 0 order
    /// </summary>
    [BeforeScenario(Order = 0)]
    public void Bootstrap()
    {
        _objectContainer.RegisterTypeAs<WebElementStorageService, IWebElementStorageService>();
    }
}