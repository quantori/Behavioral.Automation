using System.Collections.Generic;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements
{
    public interface ITableWrapper : IWebElementWrapper
    {
        IEnumerable<IWebElementWrapper> Rows { get; }

        IEnumerable<string> CellsText { get; }
    }
}