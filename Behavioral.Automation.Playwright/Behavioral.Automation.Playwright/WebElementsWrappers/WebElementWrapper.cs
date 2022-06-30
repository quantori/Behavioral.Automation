using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class WebElementWrapper : IWebElementWrapper
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