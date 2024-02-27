using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.AsyncAbstractions.UI.StepDefinitions;

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