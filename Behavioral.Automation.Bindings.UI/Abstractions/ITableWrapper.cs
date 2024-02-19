namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface ITableWrapper : IWebElement
{
    public Task ShouldBecomeVisibleAsync(int delay);
}