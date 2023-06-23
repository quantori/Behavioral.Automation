using System;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class WebElement : IWebElement
{
    public string Selector { get; set; }
    public string? Description { get; set; }

    public WebContext WebContext { get; set; }

    public ILocator Locator
    {
        get
        {
            if (WebContext is null) throw new NullReferenceException("Please set web context.");
            return WebContext.Page.Locator(Selector);
        }
    }
}