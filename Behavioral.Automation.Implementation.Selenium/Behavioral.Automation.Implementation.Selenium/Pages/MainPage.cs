using Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;
using Behavioral.Automation.Configs;
using Behavioral.Automation.Implementation.Selenium.Configs;
using Behavioral.Automation.Implementation.Selenium.Selectors;
using Behavioral.Automation.Playwright.Pages;

namespace Behavioral.Automation.Implementation.Selenium.Pages;

class MainPageExample : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;

    public ElementSelector DemoLabel = new() {IdSelector = "label-simple-text"};
    public ButtonSelector IncrementCountButton = new() {XpathSelector = "//button[@data-automation-id='increment-count-button']"};

}