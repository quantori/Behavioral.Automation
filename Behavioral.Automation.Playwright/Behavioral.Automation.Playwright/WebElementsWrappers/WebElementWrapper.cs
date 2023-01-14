using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
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
    public async Task ShouldBecomeVisibleAsync()
    {
        await Assertions.Expect(Locator).ToBeVisibleAsync();
    }

    public async Task ShouldNotBecomeVisibleAsync()
    {
        await Assertions.Expect(Locator).Not.ToBeVisibleAsync();
    }

    public string Caption { get; }
}