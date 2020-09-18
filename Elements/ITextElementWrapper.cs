using System.Diagnostics.CodeAnalysis;

namespace Behavioral.Automation.Elements
{
    /// <summary>
    /// Interface used for interaction with inputs or text fields
    /// </summary>
    public interface ITextElementWrapper : IWebElementWrapper
    {
        void EnterString([NotNull] string input);

        void ClearInput();
    }
}
