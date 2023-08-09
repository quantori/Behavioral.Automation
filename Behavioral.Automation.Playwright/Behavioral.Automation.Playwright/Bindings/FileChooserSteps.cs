using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Playwright.Bindings;

[Binding]
public class FileChooserSteps
{
    private readonly WebContext context;

    public FileChooserSteps(WebContext context)
    {
        this.context = context;
    }

    [When(@"user uploads ""(.*)"" after clicking on ""(.*)"" button")]
    public async Task WhenUserUploadsAfterClickingOnButton(string file, IButtonElement button)
    {
        var fileChooser = await context.Page.RunAndWaitForFileChooserAsync(async () =>
        {
            await button.ClickAsync();
        });
        await fileChooser.SetFilesAsync(file);
    }
}