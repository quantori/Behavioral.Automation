using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
{
    public interface IListWrapper : IWebElementWrapper
    {
        IEnumerable<string> ListValues { [NotNull, ItemNotNull] get; }
    }
}
