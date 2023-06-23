using Behavioral.Automation.Playwright.Context;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers.Interface;

public interface IWebElement
{
    public WebContext WebContext { get; set; }
    public string Selector { get; set; }
    public string Description { get; set; }
}