namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IDropdownElement : IWebElement
{
    public Task OpenDropdownAndSelectAsync(string item);
}