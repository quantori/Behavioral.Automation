using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class DropdownWrapper : WebElementWrapper, IDropdownWrapper
{
    public DropdownWrapper(WebContext webContext, ILocator locator, ILocator itemLocator, string caption) :
        base(webContext, locator, caption)
    {
        Items = itemLocator;
    }
    public ILocator Items { get; set; }

    public Task<IReadOnlyList<string>> ItemsTexts => Items.AllTextContentsAsync();

    public async Task SelectValue(params string[] elements)
    {
        await Locator.ClickAsync();
        foreach (var element in elements)
        {
            var optionToClick = GetOption(element);
            await optionToClick.ClickAsync();
        }
    }

    public ILocator GetOption(string option)
    {
        return Items.Filter(new LocatorFilterOptions {HasTextString = option});
    }
}