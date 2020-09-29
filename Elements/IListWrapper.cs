using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Interface used for list wrapper implementation
    /// </summary>
    public interface IListWrapper : IWebElementWrapper
    {
        IEnumerable<string> ListValues { [NotNull, ItemNotNull] get; }
    }
}
