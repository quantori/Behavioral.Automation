namespace Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

public interface IButtonElement : IWebElement
{
    public Task ClickAsync();
}