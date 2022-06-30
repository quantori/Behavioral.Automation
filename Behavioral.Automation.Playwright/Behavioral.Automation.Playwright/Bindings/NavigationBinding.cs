using System;
using System.Threading.Tasks;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Context;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class NavigationBinding
{
    private readonly WebContext _webContext;

    public NavigationBinding(WebContext webContext)
    {
        _webContext = webContext;
    }

    [Given(@"URL ""(.+?)"" is opened")]
    public async Task GivenUrlIsOpened(string url)
    {
        if (IsAbsoluteUrl(url))
        {
            await _webContext.Page.GotoAsync(url);
            return;
        }

        await _webContext.Page.GotoAsync(ConfigManager.GetConfig<Config>().BaseUrl + url,
            new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle, Timeout = 300000 });
    }

    private static bool IsAbsoluteUrl(string url)
    {
        return Uri.IsWellFormedUriString(url, UriKind.Absolute);
    }

    [Given("application URL is opened")]
    public async Task GivenApplicationUrlIsOpened()
    {
        await _webContext.Page.GotoAsync(ConfigManager.GetConfig<Config>().BaseUrl,
            new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle, Timeout = 300000 });
    }
}