namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface ILabelElement : IWebElement
{
    public Task ShouldHaveTextAsync(string text);
}