using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

namespace Behavioral.Automation.Implementation.Selenium.WebElements;

public class ButtonElement : IButtonElement
{
    public WebContext WebContext { get; }
    public ElementSelector ElementSelector { get; }
    public string? Description { get; set; }
    public Task ShouldBecomeVisibleAsync()
    {
        throw new NotImplementedException();
    }

    public Task ClickAsync()
    {
        throw new NotImplementedException();
    }
}