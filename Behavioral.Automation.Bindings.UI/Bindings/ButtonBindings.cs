using Behavioral.Automation.Bindings.UI.Abstractions;
using Behavioral.Automation.Bindings.UI.Context;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings.UI.Bindings;

[Binding]
public class ButtonBindings
{

    private readonly WebContext _webContext;

    public ButtonBindings(WebContext webContext)
    {
        _webContext = webContext;
    }

    [Given(@"""(.+?)"" button has been clicked")]
    [When(@"user clicks on ""(.+?)"" button")]
    public async Task ClickOnButton(IButtonElement button)
    {
        await button.ClickAsync();
    }
    
    [Then(@"""(.+?)"" button should become visible")]
    public async Task ButtonShouldBecomeVisible(IButtonElement button)
    {
        await button.IsVisibleAsync();
    }
    
    [When(@"user uploads ""(.*)"" file after clicking on ""(.*)"" button")]
    public async Task WhenUserUploadsFileAfterClickingOnButton(string filePath, IButtonElement button)
    {
        var fileChooser = await _webContext.Page.RunAndWaitForFileChooserAsync(async () =>
        {
            await button.ClickAsync();
        });
        await fileChooser.UploadFileAsync(filePath);
    }
}