namespace Behavioral.Automation.Playwright.ElementSelectors;

public class DropdownSelector : ElementSelector
{
    public ElementSelector BaseElementSelector { get; set; }
    
    public ElementSelector ItemSelector { get; set; }
}