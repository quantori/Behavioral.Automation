namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface ILabelElement
{
    public Task ShouldHaveTextAsync(string text);
    public Task ShouldBecomeVisibleAsync();
}