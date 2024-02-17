namespace Behavioral.Automation.Bindings.UI.Abstractions;

public interface IDropdownElement
{
    public Task OpenDropdownAndSelectAsync(string item);
}