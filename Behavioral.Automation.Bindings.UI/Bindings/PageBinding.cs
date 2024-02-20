using Behavioral.Automation.Bindings.UI.Context;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings.UI.Bindings;

[Binding]
public class PageBinding
{
    private readonly WebContext _webContext;

    public PageBinding(WebContext webContext)
    {
        _webContext = webContext;
    }

    [Then(@"page title should become ""(.+?)""")]
    public async Task ThenPageTitleShouldBe(string title)
    {
        await _webContext.Page.HaveTitleAsync(title);
    }
}