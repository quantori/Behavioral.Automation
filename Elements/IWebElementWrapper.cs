using System.Collections.Generic;
using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Interface for basic web element implementation
    /// </summary>
    public interface IWebElementWrapper
    {
        IWebElement Element { get; }

        string Caption { get; }

        public string Text { get; }

       public string GetAttribute([NotNull] string attribute);

       public void Click();

       public void MouseHover();

       public IEnumerable<IWebElementWrapper> FindSubElements([NotNull] By locator, [CanBeNull] string caption);

       public void SendKeys(string text);

       public bool Displayed { get; }

       public bool Enabled { get; }

       public string Tooltip { get; }
       
       public bool Stale { get; }
    }
}
