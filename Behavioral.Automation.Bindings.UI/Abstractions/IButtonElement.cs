namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IButtonElement : IWebElement
{
    public Task ClickAsync();
}