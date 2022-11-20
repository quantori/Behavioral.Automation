using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class ClickBinding
{
    /// <summary>
    /// Execute click on the element
    /// </summary>
    /// <param name="element">Tested web element wrapper</param>
    /// <example>When user clicks on "Test" button</example>
    [Given(@"user clicked on ""(.+?)""")]
    [When(@"user clicks on ""(.+?)""")]
    public async Task ClickOnElement(WebElementWrapper element)
    {
        await element.Locator.ClickAsync();
    }

    /// <summary>
    /// Execute double click on the element
    /// </summary>
    /// <param name="element">Tested web element wrapper</param>
    /// <example>When user clicks twice on "Test" button</example>  
    [Given(@"user clicked twice on ""(.+?)""")]
    [When(@"user clicks twice on ""(.+?)""")]
    public async Task ClickTwiceOnElement(WebElementWrapper element)
    {
        await element.Locator.DblClickAsync();
    }
    
    /// <summary>
    /// Execute click on the specific element in the collection
    /// </summary>
    /// <param name="index">Number of the tested element in the collection</param>
    /// <param name="element">Tested web element wrapper</param>
    /// <example>When user clicks at first element among "Test" buttons (note that numbers from 1 to 10 can be written as words)</e
    [Given(@"user clicked at (.+?) element among ""(.+?)""")]
    [When(@"user clicks at (.+?) element among ""(.+?)""")]
    public async Task ClickByIndex(int index, WebElementWrapper element)
    {
        await element.Locator.Nth(index).ClickAsync();
    }
    
    /// <summary>
    /// Hover mouse over element
    /// </summary>
    /// <param name="element">Tested web element wrapper</param>
    /// <example>When user hovers mouse over "Test" button</example>
    [Given(@"user hovered mouse over ""(.+?)""")]
    [When(@"user hovers mouse over ""(.+?)""")]
    public async Task HoverMouse(WebElementWrapper element)
    {
        await element.Locator.HoverAsync();
    }
}