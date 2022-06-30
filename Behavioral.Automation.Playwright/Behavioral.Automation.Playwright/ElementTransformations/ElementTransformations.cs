using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Behavioral.Automation.Playwright.Utils;
using Behavioral.Automation.Playwright.WebElementsWrappers;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.ElementTransformations;

[Binding]
public class ElementTransformations
{
    private readonly WebContext _webContext;
    private readonly ILocatorProvider _locatorProvider;
    private readonly LocatorStorageService _locatorStorageService;

    public ElementTransformations(WebContext webContext, ILocatorProvider locatorProvider,
        LocatorStorageService locatorStorageService)
    {
        _webContext = webContext;
        _locatorProvider = locatorProvider;
        _locatorStorageService = locatorStorageService;
    }

    [StepArgumentTransformation]
    public IWebElementWrapper GetElement(string caption)
    {
        var selector = _locatorStorageService.Get<ElementSelector>(caption);
        return new WebElementWrapper(_webContext, _locatorProvider.GetLocator(selector), caption);
    }

    [StepArgumentTransformation]
    public IDropdownWrapper GetDropdownElement(string caption)
    {
        var dropdownSelector = _locatorStorageService.Get<DropdownSelector>(caption);
        return new DropdownWrapper(_webContext, _locatorProvider.GetLocator(dropdownSelector.BaseElementSelector),
            _locatorProvider.GetLocator(dropdownSelector.ItemSelector), caption);
    }

    [StepArgumentTransformation]
    public ITableWrapper GetTableElement(string caption)
    {
        var tableSelector = _locatorStorageService.Get<TableSelector>(caption);
        return new TableWrapper(_webContext,
            _locatorProvider.GetLocator(tableSelector.BaseElementSelector),
            _locatorProvider.GetLocator(tableSelector.RowSelector),
            tableSelector.CellSelector,
            _locatorProvider.GetLocator(tableSelector.HeaderCellSelector), 
            caption);
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
    [StepArgumentTransformation, NotNull]
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