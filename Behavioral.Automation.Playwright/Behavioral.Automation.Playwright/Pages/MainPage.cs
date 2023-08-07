using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.WebElementsWrappers;

namespace Behavioral.Automation.Playwright.Pages;

class MainPageExample : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;

    public InputElement TemplateInput = new InputElement() { Selector = "//textarea[@id='seq']"};
    public ButtonElement ChooseTemplateFileButton = new ButtonElement() {Selector = "//input[@id='upl']"};
    public InputElement ForwardPrimerFromInput = new InputElement() {Selector = "//input[@name='PRIMER5_START']"};
    public InputElement ForwardPrimerToInput = new InputElement() {Selector = "//input[@name='PRIMER5_END']"};
    public InputElement ReversePrimerFromInput = new InputElement() {Selector = "//input[@name='PRIMER3_START']"};
    public InputElement ReversePrimerToInput = new InputElement() {Selector = "//input[@name='PRIMER3_END']"};
    public InputElement ForwardPrimerInput = new InputElement() {Selector = "//input[@name='PRIMER_LEFT_INPUT']"};
    public InputElement ReversePrimerInput = new InputElement() {Selector = "//input[@name='PRIMER_RIGHT_INPUT']"};
    
    //primer parameters block:
    public InputElement MinimalSizeOfPCRProductInput =
        new InputElement() {Selector = "//*[@name='PRIMER_PRODUCT_MIN']"};
    public InputElement MaximalSizeOfPCRProductInput =
        new InputElement() {Selector = "//*[@name='PRIMER_PRODUCT_MAX']"};
    public InputElement NumberOfPrimersToReturnInput =
        new InputElement() {Selector = "//*[@name='PRIMER_NUM_RETURN']"};
    public InputElement MinimalMeltingTemperatureOfPrimersInput =
        new InputElement() {Selector = "//*[@name='PRIMER_MIN_TM']"};
    public InputElement OptimalMeltingTemperatureOfPrimersInput =
        new InputElement() {Selector = "//*[@name='PRIMER_OPT_TM']"};
    public InputElement MaximalMeltingTemperatureOfPrimersInput =
        new InputElement() {Selector = "//*[@name='PRIMER_MAX_TM']"};
    public InputElement MaximalMeltingTemperatureDifferenceInput =
        new InputElement() {Selector = "//*[@name='PRIMER_MAX_DIFF_TM']"};
    
    public DropdownElement ExonJunctionSpanDropdown = new()
    {
        Selector = "//select[@name='PRIMER_ON_SPLICE_SITE']",
        MenuSelector = "//select[@name='PRIMER_ON_SPLICE_SITE']",
        ItemSelector = "//option",
        ItemSelectionSelector = "//option"
    };
    
    public InputElement MinSiteOverlapByFivePrimeEndInput =
        new InputElement() {Selector = "//*[@name='SPLICE_SITE_OVERLAP_5END']"};
    public InputElement MinSiteOverlapByThreePrimeEndInput =
        new InputElement() {Selector = "//*[@name='SPLICE_SITE_OVERLAP_3END']"};
    public InputElement MaxSiteOverlapByThreePrimeEndInput =
        new InputElement() {Selector = "//*[@name='SPLICE_SITE_OVERLAP_3END_MAX']"};
    

    public ButtonElement GetPrimersButton = new()
        {Selector = "//form/div[@class='searchInfo ']//input[@value='Get Primers']"};

    public DropdownElement DatabaseDropdown = new()
    {
        Selector = "//select[@name='PRIMER_SPECIFICITY_DATABASE']",
        MenuSelector = "//select[@name='PRIMER_SPECIFICITY_DATABASE']",
        ItemSelector = "//option",
        ItemSelectionSelector = "//option"
    };

    public LabelElement ErrorLabel = new LabelElement() {Selector = "//p[@class='error']"};

    public TableElement PrimersDesignTable = new() {Selector = "//div[@id='alignInfo']"};

    public CheckboxElement PerformSpecificityCheckCheckbox =
        new() {Selector = "//input[@name='SEARCH_SPECIFIC_PRIMER']"};
}