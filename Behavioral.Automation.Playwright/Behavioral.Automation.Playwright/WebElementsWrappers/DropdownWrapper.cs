using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using JetBrains.Annotations;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Behavioral.Automation.Playwright.WebElementsWrappers;

public class DropdownWrapper : WebElement, IDropdownElement
{
    public string MenuSelector { get; set; }
    public ILocator Menu => WebContext.Page.Locator(MenuSelector);
    public ILocator ItemSelection => Menu.Locator(ItemSelectionSelector);
    public string ItemSelectionSelector { get; set; }

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

    public async Task OpenDropdownAndSelectAsync(string itemText)
    {
        await Locator.ClickAsync();
        await SelectDropdownOptionAsync(itemText);
    }
    
    public async Task SelectDropdownOptionAsync(string itemText)
    {
        var index = await GetDropdownOptionIndexByTextAsync(itemText, true);
        await ItemSelection.Nth(index).ClickAsync();
    }
    
    private async Task<int> GetDropdownOptionIndexByTextAsync(string optionText, bool throwAssert)
    {
        Console.WriteLine($"Trying to get 'AllTextContentsAsync' from locator {Items}");
        var allTextContents = await Items.AllTextContentsAsync();
        var index = allTextContents.ToList()
            .FindIndex(s => s.Equals(optionText, StringComparison.InvariantCultureIgnoreCase));
        for (var i = 0; i < 3; i++)
        {
            if (index != -1) break;
            Thread.Sleep(3000);
            allTextContents = await Items.AllTextContentsAsync();
            index = allTextContents.ToList()
                .FindIndex(s => s.Equals(optionText, StringComparison.InvariantCultureIgnoreCase));
        }

        if (throwAssert)
        {
            Assert.Fail($"Can't find dropdown option {optionText}");
        }

        return index;
    }
}