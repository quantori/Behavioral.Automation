using System.Threading.Tasks;
using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Playwright.Selectors;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class ButtonElement: PlaywrightWebElement, IButtonElement
{
    
    public ButtonElement(WebContext webContext, ButtonSelector selector) : base(webContext, selector)
    {
    }

    public async Task ClickAsync()
    {
        await Locator.ClickAsync();
    }
}