using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements.Interfaces
{
    public interface IMultiSelectDropdownWrapper : IDropdownWrapper
    {
        IEnumerable<string> SelectedValuesTexts { [NotNull] get; }
    }
}
