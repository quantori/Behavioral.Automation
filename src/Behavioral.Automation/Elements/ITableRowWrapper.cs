using System.Collections.Generic;

namespace Behavioral.Automation.Elements
{
    public interface ITableRowWrapper: IWebElementWrapper
    {
        IEnumerable<string> CellsText { get; }
    }
}