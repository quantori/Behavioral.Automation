using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;

namespace Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

public interface IWebElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }
    public Task ShouldBecomeVisibleAsync();
}