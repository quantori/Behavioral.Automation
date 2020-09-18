using System.Collections.Generic;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Interface used for interaction with tables
    /// </summary>
    public interface ITableWrapper : IWebElementWrapper
    {
        IEnumerable<IWebElementWrapper> Rows { get; }

        IEnumerable<string> CellsText { get; }
    }
}