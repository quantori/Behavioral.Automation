namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IInputWebElement
{
    public Task TypeAsync(string text);
}