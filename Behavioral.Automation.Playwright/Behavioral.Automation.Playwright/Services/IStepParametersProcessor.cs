using System.Text.RegularExpressions;

namespace Behavioral.Automation.Playwright.Services
{
    /// <summary>
    /// This interface contains methods which convert objects passed to ComplexBindingBuilder into step expression to use in Runner
    /// </summary>
    public interface IStepParametersProcessor
    {
        string CreateStepExpression(string pattern, params object[] arguments);
        public bool TryParseStepExpressionArguments(Regex regex, string stepExpression, out string[] arguments);
    }
}