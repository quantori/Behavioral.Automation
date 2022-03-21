using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Playwright;

namespace Behavioral.Automation.Playwright.Services
{
    public interface IElementSelectionService
    {
        /// <summary>
        /// Find web element by caption
        /// </summary>
        /// <param name="caption"></param>
        /// <returns>IElementHandle object</returns>
        [CanBeNull]
        IElementHandle Find([NotNull] string caption);

        IEnumerable<IElementHandle> FindMultipleElements([NotNull] string caption);
    }
}