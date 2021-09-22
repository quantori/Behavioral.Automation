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
    }
}
