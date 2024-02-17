namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface ITableWrapper
{
    public Task ShouldBecomeVisibleAsync();
    public Task ShouldBecomeVisibleAsync(int delay);
}