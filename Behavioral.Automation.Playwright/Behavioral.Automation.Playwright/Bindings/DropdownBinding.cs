using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

/// <summary>
/// Dropdown steps. Should be merged with collection of dropdown?
/// </summary>
[Binding]
public class DropdownBinding
{
    private readonly LocatorStorageService _locatorStorageService;

    /// <type>Basic</type>
    [Given(@"user opened ""(.*)"" dropdown and selected ""(.*)"" in dropdown menu")]
    [When(@"user opens ""(.*)"" dropdown and selects ""(.*)"" in dropdown menu")]
    public async Task WhenUserSelectsInDropdownRegex(IDropdownElement element, string itemText)
    {
        await element.OpenDropdownAndSelectAsync(itemText);
    }
}