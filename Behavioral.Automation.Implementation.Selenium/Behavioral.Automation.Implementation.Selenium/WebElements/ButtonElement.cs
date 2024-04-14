using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Implementation.Selenium.Selectors;

namespace Behavioral.Automation.Implementation.Selenium.WebElements;

public class ButtonElement: SeleniumWebElement, IButtonElement
{

    public ButtonElement(WebContext webContext, ButtonSelector selector) : base(webContext, selector)
    {
    }

    public Task ClickAsync()
    {
        return new Task(() => { Locator.Click(); });
    }
}