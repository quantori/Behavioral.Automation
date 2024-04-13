using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Implementation.Selenium.Configs;
using OpenQA.Selenium;

namespace Behavioral.Automation.Implementation.Selenium.Bindings;

public class Page : IPage
{
    public IWebDriver driver;
    public Task GoToUrlAsync(string url)
    {
        return new Task(() => { driver.Navigate().GoToUrl(url); });
    }

    public Task GoToApplicationUrlAsync()
    {
        return new Task(() => { driver.Navigate().GoToUrl(ConfigManager.GetConfig<Config>().BaseUrl); });
    }

    public Task HaveTitleAsync(string title)
    {
        throw new NotImplementedException();
    }
}