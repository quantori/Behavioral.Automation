using System.Collections.Generic;

namespace Behavioral.Automation.Elements
{
    public interface ITableWrapper : IWebElementWrapper
    {
        IEnumerable<ITableRowWrapper> Rows { get; }

    }
}