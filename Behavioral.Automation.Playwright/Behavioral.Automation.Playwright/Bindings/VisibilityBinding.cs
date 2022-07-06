using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class VisibilityBinding
{
    private readonly ElementTransformations.ElementTransformations _elementTransformations;

    public VisibilityBinding(ElementTransformations.ElementTransformations elementTransformations)
    {
        _elementTransformations = elementTransformations;
    }
    
    [Given(@"the ""(.+?)"" (is|is not) visible")]
    [Then(@"the ""(.+?)"" should (be|be not) visible")]
    public async Task CheckElementVisibility(IWebElementWrapper element, string condition)
    {
        if (!condition.Contains("not"))
        {
            await Assertions.Expect(element.Locator).ToBeVisibleAsync();
        }
        else
        {
            await Assertions.Expect(element.Locator).Not.ToBeVisibleAsync();
        }
    }

    [Given("the following controls (are|are not) visible:")]
    [Then("the following controls should (be|be not) visible:")]
    public async Task CheckMultipleControlsVisibility(string condition, Table table)
    {
        foreach (var row in table.Rows)
        {
            await CheckElementVisibility(_elementTransformations.GetElement(row.Values.First()), condition);
        }
    }
}