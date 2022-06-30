using System;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Services;

public class LocatorProvider : ILocatorProvider
{
    private readonly LocatorStorageService _locatorStorageService;
    private readonly WebContext _webContext;
    private readonly string _searchAttribute = ConfigManager.GetConfig<Config>().SearchAttribute;

    public LocatorProvider(LocatorStorageService locatorStorageService, WebContext webContext)
    {
        _locatorStorageService = locatorStorageService;
        _webContext = webContext;
    }

    public ILocator GetLocator(ElementSelector selector)
    {
        if (selector.IdSelector != null)
        {
            return _webContext.Page.Locator($"//*[@{_searchAttribute}='{selector.IdSelector}']");
        }

        return selector.XpathSelector != null ? _webContext.Page.Locator(selector.XpathSelector) : 
            throw new NullReferenceException("Element was not found or web context is null");
    }
}