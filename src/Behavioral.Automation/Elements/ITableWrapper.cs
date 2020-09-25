using System.Collections.Generic;

namespace Behavioral.Automation.Elements
{
    public interface ITableWrapper : IWebElementWrapper
    {
        IEnumerable<IWebElementWrapper> Rows { get; }

        IEnumerable<string> CellsText { get; }
    }
}