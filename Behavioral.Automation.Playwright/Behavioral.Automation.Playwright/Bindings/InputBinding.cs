using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class InputBinding
{

    /// <summary>
    /// Enter string into text field or input
    /// </summary>
    /// <param name="input">String to enter</param>
    /// <param name="element">Tested web element wrapper</param>
    /// <example>When user enters "test string" into "Test input"</example>
    [Given(@"user entered ""(.+?)"" into ""(.+?)"" input")]
    [When(@"user enters ""(.+?)"" into ""(.+?)"" input")]
    public async Task FillInput(string text, IInputWebElement element)
    {
        await element.TypeAsync(text);
    }
}