using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Interface used for interaction with lists
    /// </summary>
    public interface IListWrapper : IWebElementWrapper
    {
        IEnumerable<string> ListValues { [NotNull, ItemNotNull] get; }
    }
}
