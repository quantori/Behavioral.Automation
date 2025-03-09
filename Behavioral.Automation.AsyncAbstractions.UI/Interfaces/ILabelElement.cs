namespace Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

public interface ILabelElement : IWebElement
{
    public Task ShouldHaveTextAsync(string text);
    public Task ShouldNotHaveTextAsync(string text);
}