using System.Collections.Generic;

namespace Behavioral.Automation.Elements.Interfaces
{
    public interface ITableWrapper : IWebElementWrapper
    {
        IEnumerable<ITableRowWrapper> Rows { get; }

    }
}