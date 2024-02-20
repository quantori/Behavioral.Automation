using Behavioral.Automation.Bindings.UI.Context;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings.UI.Bindings;

[Binding]
public class NavigationBinding
{
    private readonly WebContext _webContext;

    public NavigationBinding(WebContext webContext)
    {
        _webContext = webContext;
    }

    [Given(@"URL ""(.+?)"" was opened")]
    [When(@"user opens URL ""(.+?)""")]
    public async Task GivenUrlIsOpened(string url)
    {
        await _webContext.Page.GoToUrlAsync(url);
    }

    [Given("application URL was opened")]
    public async Task GivenApplicationUrlIsOpened()
    {
        await _webContext.Page.GoToApplicationUrlAsync();
    }
}