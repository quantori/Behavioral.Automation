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

public class DropdownElement : WebElement, IDropdownElement
{
    public string MenuSelector { get; set; }
    public ILocator Menu => WebContext.Page.Locator(MenuSelector);
    public ILocator ItemSelection => Menu.Locator(ItemSelectionSelector);
    public string ItemSelectionSelector { get; set; }
    public string ItemSelector { get; set; }


    public ILocator Items
    {
        get
        {
            if (WebContext is null) throw new NullReferenceException("Please set web context.");
            return Menu.Locator(ItemSelector);
        }
        set => throw new NotImplementedException();
    }

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
        await Locator.ClickAsync();
    }
    
    public async Task SelectDropdownOptionAsync(string itemText)
    {
        Thread.Sleep(1000);
        var index = await GetDropdownOptionIndexByTextAsync(itemText, true);
        await Locator.Locator(ItemSelector).Nth(0).EvaluateAsync("node => node.removeAttribute('selected')");
        await Locator.Locator(ItemSelector).Nth(index).EvaluateAsync("node => node.setAttribute('selected','selected')");
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
            if (index == -1) Assert.Fail($"Can't find dropdown option '{optionText}'. Available options are '{string.Join("', '", allTextContents.ToList())}'");
        }

        return index;
    }
}