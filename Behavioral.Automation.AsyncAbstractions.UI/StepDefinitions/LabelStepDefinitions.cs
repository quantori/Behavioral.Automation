using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.AsyncAbstractions.UI.StepDefinitions;

[Binding]
public class LabelStepDefinitions
{
    /// <summary>
    /// Check that element's text is equal to the expected one
    /// </summary>
    /// <param name="label">Tested label object</param>
    /// <param name="type">Assertion behavior</param>
    /// <param name="value">Expected value</param>
    /// <example>Then "Test" element text should be "expected text"</example>
    [Given("the \"(.*?)\" label text (is|is not) \"(.*)\"")]
    [Then("the \"(.*?)\" label text should (become|become not) \"(.*)\"")]
    public async Task CheckSelectedText(ILabelElement label, string type, string value)
    {
        if (type.Equals("is") || type.Equals("become"))
        {
            await label.ShouldHaveTextAsync(value);
        }
        else
        {
            await label.ShouldNotHaveTextAsync(value);
        }
    }

    [Then(@"the ""(.*)"" label should become visible")]
    public async Task CheckLabelVisibility(ILabelElement label)
    {
        await label.ShouldBecomeVisibleAsync();
    }
}