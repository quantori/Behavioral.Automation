namespace Behavioral.Automation.Interfaces.UI.Abstractions;

public interface IButtonElement : IWebElement
{
    public Task ClickAsync();
}