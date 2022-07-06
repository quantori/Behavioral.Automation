namespace Behavioral.Automation.Playwright.Services.ElementSelectors;

public class TableSelector : ElementSelector
{
    public ElementSelector BaseElementSelector { get; set; }
    public ElementSelector RowSelector { get; set; }
    public ElementSelector? CellSelector { get; set; }
    public ElementSelector? HeaderCellSelector { get; set; }
}