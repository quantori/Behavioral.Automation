using Behavioral.Automation.Bindings.UI.Abstractions;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings.UI.Bindings;

[Binding]
public class DropdownBinding
{

    [Given(@"""(.*)"" dropdown was opened and ""(.*)"" was selected in dropdown menu")]
    [When(@"user opens ""(.*)"" dropdown and selects ""(.*)"" in dropdown menu")]
    public async Task WhenUserSelectsInDropdownRegex(IDropdownElement dropdown, string itemText)
    {
        await dropdown.OpenDropdownAndSelectAsync(itemText);
    }
}