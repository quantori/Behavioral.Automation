using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Implementation.Selenium.Bindings;

namespace Behavioral.Automation.Implementation.Selenium.WebElements;

public class SeleniumWebElement: IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }
    public async Task ShouldBecomeVisibleAsync()
    {
        await Assertions.Expect(Locator).ToBeVisibleAsync();
    }

    protected SeleniumWebElement(WebContext webContext, ElementSelector baseSelector)
    {
        ElementSelector = baseSelector;
        WebContext = webContext;
    }

    public OpenQA.Selenium.IWebElement Locator
    {
        get
        {
            if (WebContext is null) throw new NullReferenceException("Please set web context.");
            // Locator for Playwright can be retrieved from Page element:
            var selector = (ElementSelector.XpathSelector != null)
                ? ElementSelector.XpathSelector
                : ElementSelector.IdSelector;
            return ((Page) WebContext.Page).GetPlaywrightPage().Locator(selector);
        }
    }
}