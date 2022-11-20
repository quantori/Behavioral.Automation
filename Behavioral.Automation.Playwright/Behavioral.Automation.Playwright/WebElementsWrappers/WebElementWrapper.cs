using System;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.ElementSelectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class WebElementWrapper 
{
    private readonly string _searchAttribute = ConfigManager.GetConfig<Config>().SearchAttribute;
    public WebContext WebContext { get; }
    public ElementSelector Selector { get; }
    public ILocator Locator => GetLocator(Selector);
    public string Caption { get; }

    public WebElementWrapper(WebContext webContext, ElementSelector selector, string caption)
    {
        WebContext = webContext;
        Selector = selector;
        Caption = caption;
    }

    public ILocator GetLocator(ElementSelector selector)
    {
        if (selector.IdSelector != null)
        {
            return WebContext.Page.Locator($"//*[@{_searchAttribute}='{selector.IdSelector}']");
        }

        return selector.Selector != null ? WebContext.Page.Locator(selector.Selector) : 
            throw new NullReferenceException("Element was not found or web context is null");
    }
    
    public ILocator GetChildLocator(ElementSelector selector)
    {
        if (selector.IdSelector != null)
        {
            return Locator.Locator($"//*[@{_searchAttribute}='{selector.IdSelector}']");
        }

        return selector.Selector != null ? Locator.Locator(selector.Selector) : 
            throw new NullReferenceException("Element was not found or web context is null");
    }
    
    public ILocator GetChildLocator(ILocator parentLocator, ElementSelector selector)
    {
        if (selector.IdSelector != null)
        {
            return parentLocator.Locator($"//*[@{_searchAttribute}='{selector.IdSelector}']");
        }

        return selector.Selector != null ? parentLocator.Locator(selector.Selector) : 
            throw new NullReferenceException("Element was not found or web context is null");
    }
}