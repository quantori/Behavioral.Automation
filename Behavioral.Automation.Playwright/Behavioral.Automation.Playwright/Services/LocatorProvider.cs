using System;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.ElementSelectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Services;

public class LocatorProvider
{
    private readonly WebContext _webContext;
    private readonly string _searchAttribute = ConfigManager.GetConfig<Config>().SearchAttribute;

    public LocatorProvider(WebContext webContext)
    {
        _webContext = webContext;
    }

    public ILocator GetLocator(ElementSelector selector)
    {
        if (selector.IdSelector != null)
        {
            return _webContext.Page.Locator($"//*[@{_searchAttribute}='{selector.IdSelector}']");
        }

        return selector.Selector != null ? _webContext.Page.Locator(selector.Selector) : 
            throw new NullReferenceException("Element was not found or web context is null");
    }
}