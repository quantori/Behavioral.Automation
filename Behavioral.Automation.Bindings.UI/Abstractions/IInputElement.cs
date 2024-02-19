namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IInputElement : IWebElement
{
    public Task TypeAsync(string text);
}