using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

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
        await Assertions.Expect(_webContext.Page).ToHaveTitleAsync(title, new PageAssertionsToHaveTitleOptions() {Timeout = 60000});
    }
}