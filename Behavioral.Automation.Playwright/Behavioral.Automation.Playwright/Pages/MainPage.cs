using Behavioral.Automation.Playwright.Selectors;

namespace Behavioral.Automation.Playwright.Pages;

class MainPageExample : ISelectorStorage
{
    public ButtonSelector IncrementCountButton = new() {XpathSelector = "//button[@data-automation-id='increment-count-button']"};
    public LabelSelector DemoLabel = new() {IdSelector = "label-simple-text"};
    public LabelSelector CountQuantityLabel = new LabelSelector() {IdSelector = "count-quantity-label"};
}