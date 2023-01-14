using System;
using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Services;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.StepDefinitions;

[Binding]
public class ElementCollectionBinding
{
    private readonly LocatorStorageService _locatorStorageService;

    public ElementCollectionBinding(LocatorStorageService locatorStorageService)
    {
        _locatorStorageService = locatorStorageService;
    }

    [Given(@"user clicked on the ""(.+?)"" with ""(.+?)"" text")]
    [When(@"user clicks on the ""(.+?)"" with ""(.+?)"" text")]
    public async Task ClickOnElementByText(IWebElementWrapper element, string text)
    {
        var allTextContents = await element.Locator.AllTextContentsAsync();
        var index = allTextContents.ToList().FindIndex(s => s.Equals(text, StringComparison.InvariantCultureIgnoreCase));
        if (index < 0)
        {
            Assert.Fail($"No element with text '{text}' found in '{element}' collection.");
        } 
        await element.Locator.Nth(index).ClickAsync();
    }

    [Given(@"user clicked on every ""(.*)""")]
    [When(@"user clicks on every ""(.*)""")]
    public async Task ClickOnEveryElement(IWebElementWrapper element)
    {
        var count = await element.Locator.CountAsync();
        for (var i = 0; i < count; i++)
        {
            await element.Locator.Nth(i).ClickAsync();
        }
    }

    [Given(@"user clicked on ""(.+?)"" element among ""(.+?)""")]
    [When(@"user clicks on ""(.+?)"" element among ""(.+?)""")]
    public async Task ClickOnElementByIndex(IWebElementWrapper element, int index)
    {
        await element.Locator.Nth(index).ClickAsync();
    }
}