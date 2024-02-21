using Behavioral.Automation.Interfaces.UI.Abstractions;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Interfaces.UI.ElementTransformations;

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