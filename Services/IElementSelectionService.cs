using JetBrains.Annotations;
using OpenQA.Selenium;

namespace Behavioral.Automation.Services
{
    public interface IElementSelectionService
    {
        /// <summary>
        /// Find web element by caption
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        [CanBeNull]
        IWebElement Find([NotNull] string caption);
    }
}