using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.AsyncAbstractions.UI.StepDefinitions;

[Binding]
public class ButtonStepDefinitions
{

    [Given(@"the ""(.+?)"" button has been clicked")]
    [When(@"user clicks on the ""(.+?)"" button")]
    public async Task ClickOnButton(IButtonElement button)
    {
        await button.ClickAsync();
    }
}