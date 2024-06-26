﻿using System;
using System.Threading.Tasks;
using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public abstract class PlaywrightWebElement : IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }
    public async Task ShouldBecomeVisibleAsync()
    {
        await Assertions.Expect(Locator).ToBeVisibleAsync();
    }

    protected PlaywrightWebElement(WebContext webContext, ElementSelector baseSelector)
    {
        ElementSelector = baseSelector;
        WebContext = webContext;
    }

    public ILocator Locator
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