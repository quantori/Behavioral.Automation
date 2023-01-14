using System.Linq;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.StepDefinitions;

[Binding]
public class WebElementBindings
{
    private readonly ElementTransformations.ElementTransformations _elementTransformations;

    public WebElementBindings(ElementTransformations.ElementTransformations elementTransformations)
    {
        _elementTransformations = elementTransformations;
    }

    [Given(@"the ""(.+?)"" (is|is not) visible")]
    [Then(@"the ""(.+?)"" should (be|be not) visible")]
    public async Task CheckElementVisibility(IWebElementWrapper element, string condition)
    {
        if (!condition.Contains("not"))
        {
            await element.ShouldBecomeVisibleAsync();
        }
        else
        {
            await element.ShouldNotBecomeVisibleAsync();
        }
    }

    [Given("the following elements (are|are not) visible:")]
    [Then("the following elements should (be|be not) visible:")]
    public async Task CheckMultipleElementsVisibility(string condition, Table table)
    {
        foreach (var row in table.Rows)
        {
            await CheckElementVisibility(_elementTransformations.GetElement(row.Values.First()), condition);
        }
    }
}