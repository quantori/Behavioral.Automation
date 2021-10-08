using System.Diagnostics.CodeAnalysis;

namespace Behavioral.Automation.Elements.Interfaces
{
    public interface ITextElementWrapper : IWebElementWrapper
    {
        void EnterString([NotNull] string input);

        void ClearInput();
    }
}
