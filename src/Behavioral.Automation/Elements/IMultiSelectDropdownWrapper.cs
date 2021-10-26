using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
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
