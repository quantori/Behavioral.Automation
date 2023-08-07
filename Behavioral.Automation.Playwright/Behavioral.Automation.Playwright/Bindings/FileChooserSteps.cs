using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Context;
using Behavioral.Automation.Playwright.WebElementsWrappers.Interface;
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

    [Then(@"file chooser should become visible")]
    public async Task ThenFileChooserShouldBecomeVisible()
    {
        await context.Page.FileChooser
    }
}