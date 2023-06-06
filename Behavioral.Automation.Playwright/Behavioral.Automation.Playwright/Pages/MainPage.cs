using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Services.ElementSelectors;

namespace Behavioral.Automation.Playwright.Pages;

class MainPageExample : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;

    public ElementSelector TemplateInput = new() {XpathSelector = "//textarea[@id='seq']"};

    public ElementSelector GetPrimersButton = new()
        {XpathSelector = "//form/div[@class='searchInfo ']//input[@value='Get Primers']"};
}