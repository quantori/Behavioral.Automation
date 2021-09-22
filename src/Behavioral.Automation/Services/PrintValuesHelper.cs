using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;

namespace Behavioral.Automation.Services
{
    public static class PrintValuesHelper
    {
        public static string GetPrintableValues(this IEnumerable<ITableRowWrapper> rows)
        {
            var values = rows.Aggregate("\r\n|", (current, row) => current + (row.CellsText.Aggregate((x, y) => $" {x} | {y}") + " |\r\n |"));
            return values.Remove(values.Length - 3);
        }
    }
}
