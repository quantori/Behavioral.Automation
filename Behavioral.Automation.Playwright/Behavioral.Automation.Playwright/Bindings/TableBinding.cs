using System;
using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class TableBinding
{
    private static readonly float? Timeout = ConfigManager.GetConfig<Config>().AssertTimeoutMilliseconds;

    [Then(@"""(.*)"" table should become visible$")]
    public async Task ThenTableShouldBecomeVisible(ITableWrapper table)
    {
        await table.ShouldBecomeVisibleAsync();
    }
    
    [Then(@"""(.*)"" table should become visible within ""(.*)"" seconds")]
    public async Task ThenTableShouldBecomeVisible(ITableWrapper table, int seconds)
    {
        await table.ShouldBecomeVisibleAsync(seconds);
    }
}