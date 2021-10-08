using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements.Interfaces
{
    public interface IListWrapper : IWebElementWrapper
    {
        IEnumerable<string> ListValues { [NotNull, ItemNotNull] get; }
    }
}
