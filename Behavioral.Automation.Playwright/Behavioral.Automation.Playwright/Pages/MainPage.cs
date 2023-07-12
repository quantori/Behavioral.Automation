using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.WebElementsWrappers;

namespace Behavioral.Automation.Playwright.Pages;

class MainPageExample : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;

    public InputElement TemplateInput = new InputElement() { Selector = "//textarea[@id='seq']"};
    public InputElement ForwardPrimerFromInput = new InputElement() {Selector = "//input[@name='PRIMER5_START']"};
    public InputElement ForwardPrimerToInput = new InputElement() {Selector = "//input[@name='PRIMER5_END']"};
    public InputElement ReversePrimerFromInput = new InputElement() {Selector = "//input[@name='PRIMER3_START']"};
    public InputElement ReversePrimerToInput = new InputElement() {Selector = "//input[@name='PRIMER3_END']"};
    public InputElement ForwardPrimerInput = new InputElement() {Selector = "//input[@name='PRIMER_LEFT_INPUT']"};
    public InputElement ReversePrimerInput = new InputElement() {Selector = "//input[@name='PRIMER_RIGHT_INPUT']"};
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