using System.Diagnostics.CodeAnalysis;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Interface for text element wrapper implementation (for example inputs or text fields)
    /// </summary>
    public interface ITextElementWrapper : IWebElementWrapper
    {
        void EnterString([NotNull] string input);

        void ClearInput();
    }
}
