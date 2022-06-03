using System.Diagnostics.CodeAnalysis;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Input element capable of entering and clearing text
    /// </summary>
    public interface ITextElementWrapper : IWebElementWrapper
    {
        /// <summary>
        /// Enters provided text into input element
        /// </summary>
        /// <param name="input">text to enter</param>
        void EnterString([NotNull] string input);

        /// <summary>
        /// Clears all text from input element
        /// </summary>
        void ClearInput();
    }
}
