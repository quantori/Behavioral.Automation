using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Common dropdown element actions and properties/fields
    /// </summary>
    public interface IDropdownWrapper : IWebElementWrapper
    {
        /// <summary>
        /// Selects elements with given names 
        /// </summary>
        /// <param name="elements">collection of dropdown element names to select</param>
        void Select(params string[] elements);

        /// <summary>
        /// Returns currently selected dropdown value
        /// </summary>
        string SelectedValue { [CanBeNull] get; }

        /// <summary>
        /// Collection of dropdown items as strings
        /// </summary>
        IEnumerable<string> Items { [NotNull, ItemNotNull] get; }

        /// <summary>
        /// Collection of dropdown items as <see cref="IWebElementWrapper"/>
        /// </summary>
        IEnumerable<IWebElementWrapper> Elements { [NotNull, ItemNotNull] get; }

        /// <summary>
        /// Indicates the absence of dropdown items 
        /// </summary>
        bool Empty { get; }
    }
}