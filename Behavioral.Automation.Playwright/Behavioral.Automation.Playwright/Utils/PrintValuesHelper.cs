using System.Collections.Generic;
using System.Linq;

namespace Behavioral.Automation.Playwright.Utils;

public static class PrintValuesHelper
{
    /// <summary>
    /// Method that returns error message with elements aggregation 
    /// </summary>
    /// <param name="caption">wrapper caption</param>
    /// <param name="items">collection of dropdown elements in string form</param>
    /// <returns>Error message with actual collection items</returns>
    public static string CreateDropdownErrorMessage(this IEnumerable<string> items, string caption)
    {
        caption ??= "Collection";
        if (items == null || !items.Any())
            return $"'{caption}' is empty";
            
        return $"Actual '{caption}' items are: {items.Aggregate((x, y) => $"{x}, {y}")}";
    }
}