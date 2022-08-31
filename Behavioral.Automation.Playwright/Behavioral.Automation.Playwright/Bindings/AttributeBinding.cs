using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class AttributeBinding
{
    private readonly ElementTransformations.ElementTransformations _elementTransformations;

    public AttributeBinding(ElementTransformations.ElementTransformations elementTransformations)
    {
        _elementTransformations = elementTransformations;
    }
    
    
    /// <summary>
    /// Check if element is disabled or enabled
    /// </summary>
    /// <param name="element">Tested web element wrapper</param>
    /// <param name="enabled">Element expected status (enabled or disabled)</param>
    /// <example>Then "Test" input should be enabled</example>
    [Given(@"the ""(.+?)"" is (enabled|disabled)")]
    [Then(@"the ""(.+?)"" should be| (enabled|disabled)")]
    public async Task CheckElementIsDisabled(WebElementWrapper element, bool enabled)
    {
        if (enabled)
        {
            await Assertions.Expect(element.Locator).Not.ToBeDisabledAsync();
        }
        else
        {
            await Assertions.Expect(element.Locator).ToBeDisabledAsync();
        }
    }
    
    /// <summary>
    /// Check that multiple elements are disabled or enabled
    /// </summary>
    /// <param name="enabled">Elements expected status (enabled or disabled)</param>
    /// <param name="table">Specflow table with element names to be tested</param>
    /// <example>
    /// Then the following controls should be enabled:
    /// | control       |
    /// | Test" input |
    /// | Test2 input |
    /// </example>
    [Given("the following controls are (enabled|disabled):")]
    [Then("the following controls should be (enabled|disabled):")]
    public async Task CheckControlTypeCollectionShown(bool enabled, Table table)
    {
        foreach (var row in table.Rows)
        {
            await CheckElementIsDisabled(_elementTransformations.GetElement(row.Values.First()), enabled);
        }
    }
}