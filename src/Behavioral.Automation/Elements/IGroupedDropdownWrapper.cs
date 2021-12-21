using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Dropdown with element groups common methods and properties
    /// </summary>
    public interface IGroupedDropdownWrapper : IDropdownWrapper
    {
        /// <summary>
        /// Collection of group items values represented as <see cref="IEnumerable{T}"/>
        /// </summary>
        IEnumerable<string> GroupTexts { [NotNull, ItemNotNull] get; }
    }
}
