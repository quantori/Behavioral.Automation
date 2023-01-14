using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.StepDefinitions;

[Binding]
public class InputBinding
{
    private readonly ElementTransformations.ElementTransformations _elementTransformations;

    public InputBinding(ElementTransformations.ElementTransformations elementTransformations)
    {
        _elementTransformations = elementTransformations;
    }
    
    /// <summary>
    /// Enter string into text field or input
    /// </summary>
    /// <param name="input">String to enter</param>
    /// <param name="element">Tested web element wrapper</param>
    /// <example>When user enters "test string" into "Test input"</example>
    [Given(@"user entered (.+?) into ""(.+?)""")]
    [When(@"user enters ""(.+?)"" into ""(.+?)""")]
    public async Task FillInput(string text, IWebElementWrapper element)
    {
        await element.Locator.FillAsync(text);
    }
    
    /// Enter multiple value into multiple controls
    /// </summary>
    /// <param name="table">Specflow table which stores name of controls and values to enter</param>
    /// <example>
    /// When user enters the following values into the following controls:
    /// | Value       | element            |
    /// | "Test value" | Test1 input       |
    /// | "Test value" | Test2 input       |
    /// </example>
    [Given("user entered the following values into the following controls:")]
    [When("user enters the following values into the following controls:")]
    public async Task EnterInputIntoMultipleControls(Table table)
    {
        foreach (var row in table.Rows)
        {
            await FillInput(row.Values.First(), _elementTransformations.GetElement(row.Values.Last()));
        }
    }
}