using System.Collections.Generic;
using JetBrains.Annotations;

namespace Behavioral.Automation.Elements.Interfaces
{
    /// <summary>
    /// Collection of uniform elements, i.e. table column 
    /// </summary>
    public interface IElementCollectionWrapper : IWebElementWrapper
    {
        /// <summary>
        /// Collection of elements in form of <see cref="IWebElementWrapper"/>
        /// </summary>
        IEnumerable<IWebElementWrapper> Elements { [NotNull, ItemNotNull] get; }

        /// <summary>
        /// Clicks on the element by its zero-based index
        /// </summary>
        /// <param name="index">Index of the element to click on</param>
        void ClickByIndex(int index);

        /// <summary>
        /// Attribute value for element with given index
        /// </summary>
        /// <param name="attribute">Attribute name</param>
        /// <param name="index">Element index</param>
        /// <returns>Attribute value as <see cref="string"/></returns>
        string GetAttributeByIndex([NotNull] string attribute, int index);
    }
}
