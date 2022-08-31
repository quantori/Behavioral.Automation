using System.Collections.Generic;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.ElementSelectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class DropdownWrapper : WebElementWrapper
{
    public readonly DropdownSelector DropdownSelector;

    public DropdownWrapper(WebContext webContext, DropdownSelector dropdownSelector, string caption) :
        base(webContext, dropdownSelector, caption)
    {
        DropdownSelector = dropdownSelector;
    }

    public ILocator Items => GetChildLocator(DropdownSelector.ItemSelector);
    public ILocator Base => GetChildLocator(DropdownSelector.BaseElementSelector);

    public Task<IReadOnlyList<string>> ItemsTexts => Items.AllTextContentsAsync();

    public async Task SelectValue(params string[] elements)
    {
        await Base.ClickAsync();
        foreach (var element in elements)
        {
            var optionToClick = GetOption(element);
            await optionToClick.ClickAsync();
        }
    }

    public ILocator GetOption(string option)
    {
        return Items.Filter(new LocatorFilterOptions { HasTextString = option });
    }
}