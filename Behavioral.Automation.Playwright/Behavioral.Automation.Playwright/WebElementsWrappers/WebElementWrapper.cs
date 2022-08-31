using Behavioral.Automation.Playwright.Context;

using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class WebElementWrapper 
{
    public WebElementWrapper(WebContext webContext, ILocator locator, string caption)
    {
        WebContext = webContext;
        Locator = locator;
        Caption = caption;
    }
    public WebContext WebContext { get; }
    public ILocator Locator { get; }
    public string Caption { get; }
}