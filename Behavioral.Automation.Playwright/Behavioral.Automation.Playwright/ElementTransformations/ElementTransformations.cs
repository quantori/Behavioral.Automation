using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.Utils;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.ElementTransformations;

[Binding]
public class ElementTransformations
{
    private readonly LocatorStorageService _locatorStorageService;

    public ElementTransformations(LocatorStorageService locatorStorageService)
    {
        _locatorStorageService = locatorStorageService;
    }

    [StepArgumentTransformation]
    public IDropdownElement GetDropdownElement(string caption)
    {
        var element = _locatorStorageService.Get<IDropdownElement>(caption + " Dropdown");
        return element;
    }

    [StepArgumentTransformation]
    public ITableWrapper GetTableElement(string caption)
    {
        var element = _locatorStorageService.Get<ITableWrapper>(caption + " Table");
        return element;
    }
    
    [StepArgumentTransformation]
    public IInputWebElement GetInput(string caption)
    {
        var element = _locatorStorageService.Get<IInputWebElement>(caption + " Input");
        return element;
    }
    
    [StepArgumentTransformation]
    public IButtonElement GetButtonElement(string caption)
    {
        var element = _locatorStorageService.Get<IButtonElement>(caption + " Button");
        return element;
    }
    
    [StepArgumentTransformation]
    public ICheckboxElement GetCheckboxElement(string caption)
    {
        var element = _locatorStorageService.Get<ICheckboxElement>(caption + " Checkbox");
        return element;
    }
    
    [StepArgumentTransformation]
    public ILabelElement GetLabelElement(string caption)
    {
        var element = _locatorStorageService.Get<ILabelElement>(caption + " Label");
        return element;
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