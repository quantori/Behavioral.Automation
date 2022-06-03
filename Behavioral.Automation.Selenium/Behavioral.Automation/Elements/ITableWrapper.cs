using System.Collections.Generic;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Represents table
    /// </summary>
    public interface ITableWrapper : IWebElementWrapper
    {
        /// <summary>
        /// Table rows in form of <see cref="ITableRowWrapper"/> collection
        /// </summary>
        IEnumerable<ITableRowWrapper> Rows { get; }

    }
}