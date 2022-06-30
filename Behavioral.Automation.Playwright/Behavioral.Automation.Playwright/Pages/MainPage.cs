using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;

namespace Behavioral.Automation.Playwright.Pages;

class MainPage : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;
    
}