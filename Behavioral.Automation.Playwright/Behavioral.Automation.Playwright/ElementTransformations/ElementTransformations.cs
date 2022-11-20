using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.ElementSelectors;
using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.Utils;
using Behavioral.Automation.Playwright.WebElementsWrappers;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.ElementTransformations;

[Binding]
public class ElementTransformations
{
    private readonly WebContext _webContext;
    private readonly LocatorStorageService _locatorStorageService;

    public ElementTransformations(WebContext webContext, LocatorStorageService locatorStorageService)
    {
        _webContext = webContext;
        _locatorStorageService = locatorStorageService;
    }

    [StepArgumentTransformation]
    public WebElementWrapper GetElement(string caption)
    {
        var selector = _locatorStorageService.Get<ElementSelector>(caption);
        return new WebElementWrapper(_webContext, selector, caption);
    }

    [StepArgumentTransformation]
    public DropdownWrapper GetDropdownElement(string caption)
    {
        var dropdownSelector = _locatorStorageService.Get<DropdownSelector>(caption);
        return new DropdownWrapper(_webContext, dropdownSelector, caption);
    }

    [StepArgumentTransformation]
    public TableWrapper GetTableElement(string caption)
    {
        var tableSelector = _locatorStorageService.Get<TableSelector>(caption);
        return new TableWrapper(_webContext, tableSelector, caption);
    }

    /// <summary>
    /// Transform "enabled" or "disabled" string into bool value
    /// </summary>
    /// <param name="enabled">"enabled" or "disabled" string</param>
    /// <returns></returns>
    [StepArgumentTransformation("(enabled|disabled)")]
    public bool ConvertEnabledState(string enabled)
    {
        return enabled == "enabled";
    }

    /// <summary>
    /// Convert strings into the numbers
    /// </summary>
    /// <param name="number">String with the number which is received from Specflow steps</param>
    /// <returns></returns>
    [StepArgumentTransformation]
    public int ParseNumber(string number)
    {
        return number switch
        {
            ("first") => 1,
            ("second") => 2,
            ("third") => 3,
            ("fourth") => 4,
            ("fifth") => 5,
            ("sixth") => 6,
            ("seventh") => 7,
            ("eighth") => 8,
            ("ninth") => 9,
            ("tenth") => 10,
            _ => StringExtensions.ParseNumberFromString(number)
        };
    }
}