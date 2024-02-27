using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;

[Binding]
public class ElementTransformations
{
    private readonly IWebElementStorageService _webElementStorageService;

    public ElementTransformations(IWebElementStorageService webElementStorageService)
    {
        _webElementStorageService = webElementStorageService;
    }

    [StepArgumentTransformation]
    public IButtonElement GetButtonElement(string caption)
    {
        var element = _webElementStorageService.Get<IButtonElement>(caption + "Button");
        return element;
    }
}