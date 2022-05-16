using System.Collections.Generic;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Represents single row of table
    /// </summary>
    public interface ITableRowWrapper: IWebElementWrapper
    {
        /// <summary>
        /// Row cells texts in form of <see cref="string"/> collection
        /// </summary>
        IEnumerable<string> CellsText { get; }
        
        /// <summary>
        /// Row cells in form of <see cref="IWebElementWrapper"/> collection
        /// </summary>
        IEnumerable<IWebElementWrapper> Cells { get; }
    }
}