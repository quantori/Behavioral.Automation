using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;

namespace Behavioral.Automation.Implementation.Selenium.Bindings;

public class TestClass : IPage
{
    public Task GoToUrlAsync(string url)
    {
        throw new NotImplementedException();
    }

    public Task GoToApplicationUrlAsync()
    {
        throw new NotImplementedException();
    }

    public Task HaveTitleAsync(string title)
    {
        throw new NotImplementedException();
    }
}