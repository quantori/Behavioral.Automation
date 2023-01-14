using System.Collections.Generic;
using System.Threading.Tasks;
using Behavioral.Automation.Playwright.Services.ElementSelectors;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.WebElementsWrappers.Interface;

public interface ITableWrapper: IWebElementWrapper
{
    public ILocator Rows { get; set; }
    
    public ElementSelector CellsSelector { get; set; }

    public ILocator? HeaderCells { get; set; }

    public ILocator GetCellsForRow(ILocator row);
    
    public Task ColumnsQuantityShouldBeMoreThanZeroAsync();
    
    //public Task<IReadOnlyList<string>> HeaderCellsTextAsync { get; }
}