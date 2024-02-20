using Behavioral.Automation.Bindings.UI.Abstractions;

namespace Behavioral.Automation.Bindings.UI.Tests.Configurations;

public class WebElementStorageService : IWebElementStorageService
{
    public T Get<T>(string locatorKey) where T: IWebElement
    {
        IWebElement webElement = new WebElement();
        return (T) webElement;
    }
}