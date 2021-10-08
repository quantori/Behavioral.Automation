using System.Collections.Generic;
using Behavioral.Automation.Elements.Interfaces;

namespace Behavioral.Automation.Services
{
    public delegate IEnumerable<T> LoadElementsFromCurrentViewCallback<T>() where T : IWebElementWrapper;
}
