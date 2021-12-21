using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements.Interfaces
{
    /// <summary>
    /// Dropdown with ability to select several items
    /// </summary>
    public interface IMultiSelectDropdownWrapper : IDropdownWrapper
    {
        /// <summary>
        /// Selected values in form of <see cref="string"/> collection
        /// </summary>
        IEnumerable<string> SelectedValuesTexts { [NotNull] get; }
    }
}
