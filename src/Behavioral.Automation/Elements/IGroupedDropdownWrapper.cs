using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
{
    public interface IGroupedDropdownWrapper : IDropdownWrapper
    {
        IEnumerable<string> GroupTexts { [NotNull, ItemNotNull] get; }
    }
}
