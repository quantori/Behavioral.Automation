using System.Text.RegularExpressions;

namespace Behavioral.Automation.Services
{
    public interface IStepParametersProcessor
    {
        string CreateStepExpression(string pattern, params object[] arguments);
        public bool TryParseStepExpressionArguments(Regex regex, string stepExpression, out string[] arguments);
    }
}