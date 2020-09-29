using System.Collections.Generic;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Interface used for table wrapper implementation
    /// </summary>
    public interface ITableWrapper : IWebElementWrapper
    {
        IEnumerable<IWebElementWrapper> Rows { get; }

        IEnumerable<string> CellsText { get; }
    }
}