using Behavioral.Automation.Bindings.UI.Abstractions;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings.UI.ElementTransformations;

[Binding]
public class ElementTransformations
{
    private readonly IWebElementStorageService _webElementStorageService;

    public ElementTransformations(IWebElementStorageService webElementStorageService)
    {
        _webElementStorageService = webElementStorageService;
    }

    [StepArgumentTransformation]
    public IInputWebElement GetInputElement(string caption)
    {
        var element = _webElementStorageService.Get<IInputWebElement>(caption + "Input");
        return element;
    }
    
    [StepArgumentTransformation]
    public ICheckboxElement GetCheckboxElement(string caption)
    {
        var element = _webElementStorageService.Get<ICheckboxElement>(caption + "Checkbox");
        return element;
    }
    
    [StepArgumentTransformation]
    public IButtonElement GetButtonElement(string caption)
    {
        var element = _webElementStorageService.Get<IButtonElement>(caption + "Button");
        return element;
    }
    
    [StepArgumentTransformation]
    public ITableWrapper GetTableElement(string caption)
    {
        var element = _webElementStorageService.Get<ITableWrapper>(caption + "Table");
        return element;
    }
}