using Behavioral.Automation.Bindings.UI.Abstractions;
using Behavioral.Automation.Bindings.UI.Context;

namespace Behavioral.Automation.Bindings.UI.Tests.Configurations;

public class WebElement : IWebElement, IButtonElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }
    public Task ClickAsync()
    {
        return Task.Run(() => Console.WriteLine("Clicked button"));
    }

    public Task ShouldBecomeVisibleAsync()
    {
        throw new NotImplementedException();
    }
}