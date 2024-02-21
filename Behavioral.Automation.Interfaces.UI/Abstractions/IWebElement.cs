using Behavioral.Automation.Interfaces.UI.Context;

namespace Behavioral.Automation.Interfaces.UI.Abstractions;

public interface IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }
    public bool IsVisible();
    public bool IsVisible(int delay);
}