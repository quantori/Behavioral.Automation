namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IInputElement
{
    public Task TypeAsync(string text);
}