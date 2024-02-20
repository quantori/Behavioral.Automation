using Behavioral.Automation.Bindings.UI.Abstractions;
using Behavioral.Automation.Bindings.UI.Context;

namespace Behavioral.Automation.Bindings.UI.Tests.Configurations;

/*
 * Shared code between all web Elements
 */
public class WebElement: IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }

    public async Task ShouldBecomeVisibleAsync()
    {
        // defaultDelay = 300 is just an example of idea that we will have default delay for all elements
        // and ability to specify delay in test steps if we have some taking time loading
        // (for example, because of data processing)
        var defaultDelay = 300;
        await ShouldBecomeVisibleAsync(defaultDelay);
        throw new NotImplementedException();
    }

    public async Task ShouldBecomeVisibleAsync(int delay)
    {
        throw new NotImplementedException();
    }
}