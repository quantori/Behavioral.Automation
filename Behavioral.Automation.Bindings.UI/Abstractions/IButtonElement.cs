namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IButtonElement
{
    public Task ClickAsync();
    public Task ShouldBecomeVisibleAsync();
}