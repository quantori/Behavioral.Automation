using System.Collections.Generic;

namespace Behavioral.Automation.Elements.Interfaces
{
    public interface ITableRowWrapper: IWebElementWrapper
    {
        IEnumerable<string> CellsText { get; }
    }
}