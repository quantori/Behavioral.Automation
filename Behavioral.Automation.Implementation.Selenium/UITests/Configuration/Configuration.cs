using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Implementation.Selenium.WebElements;

namespace UITests.Configuration;

[Binding]
public class Configuration
{
    // Configuration of DI and Factories should be done with order 0
    [BeforeTestRun(Order = 0)]
    public static void ConfigureUiImplementations()
    {
        IWebElementStorageService.RegisterWebElementImplementationAs<ButtonElement, IButtonElement>();
    }
}