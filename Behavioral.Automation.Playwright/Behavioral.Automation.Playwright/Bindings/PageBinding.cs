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

    [Then(@"page title should be ""(.+?)""")]
    public void ThenPageTitleShouldBe(string title)
    {
        Assertions.Expect(_webContext.Page).ToHaveTitleAsync(title);
    }
}