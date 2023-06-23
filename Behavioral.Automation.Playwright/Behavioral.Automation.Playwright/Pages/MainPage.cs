using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.WebElementsWrappers;

namespace Behavioral.Automation.Playwright.Pages;

class MainPageExample : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;

    public InputElement TemplateInput = new InputElement() { Selector = "//textarea[@id='seq']"};
    public ButtonElement GetPrimersButton = new()
        {Selector = "//form/div[@class='searchInfo ']//input[@value='Get Primers']"};

    public DropdownElement DatabaseDropdown = new()
    {
        Selector = "//select[@name='PRIMER_SPECIFICITY_DATABASE']",
        MenuSelector = "//select[@name='PRIMER_SPECIFICITY_DATABASE']",
        ItemSelector = "//option",
        ItemSelectionSelector = "//option"
    };

    public TableElement PrimersDesignTable = new() {Selector = "//div[@id='alignInfo']"};

    public CheckboxElement PerformSpecificityCheckCheckbox =
        new() {Selector = "//input[@name='SEARCH_SPECIFIC_PRIMER']"};
}