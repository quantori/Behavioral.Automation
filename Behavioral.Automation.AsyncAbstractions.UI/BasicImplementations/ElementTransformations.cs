using Behavioral.Automation.AsyncAbstractions.UI.Interfaces;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;

/// <summary>
/// Class containing transformations for SpecFlow step arguments related to web elements.
/// </summary>
[Binding]
public class ElementTransformations
{
    private readonly IWebElementStorageService _webElementStorageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ElementTransformations"/> class.
    /// </summary>
    /// <param name="webElementStorageService">The web element storage service used to instantiate WebElements.</param>
    public ElementTransformations(IWebElementStorageService webElementStorageService)
    {
        _webElementStorageService = webElementStorageService;
    }

    /// <summary>
    /// Transforms a step argument into a button element based on the caption.
    /// </summary>
    /// <param name="caption">The caption of the button element.</param>
    /// <returns>The button element matching the caption.</returns>
    [StepArgumentTransformation]
    public IButtonElement GetButtonElement(string caption)
    {
        var element = _webElementStorageService.Get<IButtonElement>(caption + "Button");
        return element;
    }
    
    /// <summary>
    /// Transforms a step argument into a label element based on the caption.
    /// </summary>
    /// <param name="caption">The caption of the label element.</param>
    /// <returns>The label element matching the caption.</returns>
    [StepArgumentTransformation]
    public ILabelElement GetLabelElement(string caption)
    {
        var element = _webElementStorageService.Get<ILabelElement>(caption + "Label");
        return element;
    }
}