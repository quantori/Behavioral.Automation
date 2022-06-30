using Behavioral.Automation.Playwright.Context;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers.Interface;

public interface IWebElementWrapper
{
    public WebContext WebContext { get; }
    
    public string Caption { get;}
    
    public ILocator Locator { get; }
}