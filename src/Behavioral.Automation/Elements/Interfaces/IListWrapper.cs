using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements.Interfaces
{
    /// <summary>
    /// List of nested elements
    /// </summary>
    public interface IListWrapper : IWebElementWrapper
    {
        /// <summary>
        /// Values in form of <see cref="string"/> collection
        /// </summary>
        IEnumerable<string> ListValues { [NotNull, ItemNotNull] get; }
    }
}
