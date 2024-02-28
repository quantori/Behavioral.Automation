namespace Behavioral.Automation.AsyncAbstractions.UI.BasicImplementations;

/// <summary>
/// Represents a class for storing selectors used in the instantiating of WebElements.
/// </summary>
public class ElementSelector
{
    public string? IdSelector { get; set; }

    public string? XpathSelector { get; set; }
}