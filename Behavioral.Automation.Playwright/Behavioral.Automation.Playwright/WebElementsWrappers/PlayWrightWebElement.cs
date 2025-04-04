using System;
using System.Threading.Tasks;
using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public abstract class PlaywrightWebElement : IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;

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
            if (ElementSelector.XpathSelector != null)
            {
                return ((Page) WebContext.Page).GetPlaywrightPage().Locator(ElementSelector.XpathSelector);
            }

            if (ElementSelector.IdSelector != null)
            {
                return ((Page) WebContext.Page).GetPlaywrightPage().Locator($"//*[@{Id}='{ElementSelector.IdSelector}']");
            }

            // TODO: Think about moving validation into element factory method or in element constructor
            throw new Exception("Please provide XpathSelector or IdSelector");
        }
    }
}