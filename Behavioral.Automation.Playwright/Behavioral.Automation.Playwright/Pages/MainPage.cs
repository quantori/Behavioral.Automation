using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Services.ElementSelectors;

namespace Behavioral.Automation.Playwright.Pages;

class MainPageExample : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;

    public ElementSelector DemoLabel = new() {IdSelector = "label-simple-text"};
}