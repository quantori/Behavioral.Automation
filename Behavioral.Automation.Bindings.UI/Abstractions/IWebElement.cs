using Behavioral.Automation.Bindings.UI.Context;

namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector  { get; }
    public string? Description { get; set; }
    public Task ShouldBecomeVisibleAsync();
    public Task ShouldBecomeVisibleAsync(int delay);
}