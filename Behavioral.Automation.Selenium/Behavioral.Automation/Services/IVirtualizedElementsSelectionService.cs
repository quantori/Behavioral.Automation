using System;
using System.Collections.Generic;
using Behavioral.Automation.Elements;

namespace Behavioral.Automation.Services
{
    public interface IVirtualizedElementsSelectionService
    {
        IEnumerable<T> FindVirtualized<T>(string caption,
            LoadElementsFromCurrentViewCallback<T> loadElementsCallback) where T : IWebElementWrapper;

        bool ControlIsVirtualizable(string caption);
    }
}
