namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IInputElement : IWebElement
{
    public Task InputTextAsync(string text);
}