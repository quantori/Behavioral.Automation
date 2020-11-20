using System.Collections.Generic;
using System.Linq;
using Behavioral.Automation.Elements;

namespace Behavioral.Automation.Services
{
    public static class PrintValuesHelper
    {
        public static string GetPrintableValues(this IEnumerable<ITableRowWrapper> rows)
        {
            return rows.SelectMany(x => x.CellsText).Aggregate((x, y) => $"{x}, {y}");
        }
    }
}
