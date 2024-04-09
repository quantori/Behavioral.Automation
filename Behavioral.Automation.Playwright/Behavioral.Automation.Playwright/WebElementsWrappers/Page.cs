using System;
using System.Threading.Tasks;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Microsoft.Playwright;
using IPage = Behavioral.Automation.AsyncAbstractions.UI.Interfaces.IPage;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class Page: IPage
{
    private readonly Microsoft.Playwright.IPage _playwrightPage;
    
    
    public Page(Microsoft.Playwright.IPage playwrightPage)
    {
        _playwrightPage = playwrightPage;
    }

    public Task GoToUrlAsync(string url)
    {
        throw new NotImplementedException();
    }

    public Task GoToApplicationUrlAsync()
    {
        return _playwrightPage.GotoAsync(ConfigManager.GetConfig<Config>().BaseUrl,
            new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle, Timeout = 300000 });
    }

    public Task HaveTitleAsync(string title)
    {
        throw new NotImplementedException();
    }

    public Microsoft.Playwright.IPage GetPlaywrightPage()
    {
        return _playwrightPage;
    }
}