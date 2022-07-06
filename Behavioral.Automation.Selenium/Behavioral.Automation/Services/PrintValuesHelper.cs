using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;
using JetBrains.Annotations;

namespace Behavioral.Automation.Services
{
    public static class PrintValuesHelper
    {
        public static string GetPrintableValues([NotNull] this IEnumerable<ITableRowWrapper> rows)
        {
            if (rows.Any())
            {
                var values = rows.Aggregate("\r\n|", (current, row) => current + (row.CellsText.Aggregate((x, y) => $" {x} | {y}") + " |\r\n |"));
                return values.Remove(values.Length - 3);
            }

            return "\r\n Rows collection was empty";
        }

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
}
