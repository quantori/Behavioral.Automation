using Behavioral.Automation.Bindings.UI.Abstractions;
using Behavioral.Automation.Bindings.UI.Context;
using Microsoft.Playwright;
using IPage = Microsoft.Playwright.IPage;

namespace Behavioral.Automation.Bindings.UI.Tests.Configurations;

/*
 * Shared code between all web Elements
 */
public class WebElement: IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }

    public Task ShouldBecomeVisibleAsync()
    {
        throw new NotImplementedException();
    }
}