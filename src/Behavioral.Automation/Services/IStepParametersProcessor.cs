using System.Text.RegularExpressions;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// This interface is used to implement StepParametersProcessor class
    /// StepParametersProcessor converts objects passed to ComplexBindingBuilder into step expression to use in Runner
    /// </summary>
    public interface IStepParametersProcessor
    {
        string CreateStepExpression(string pattern, params object[] arguments);
        public bool TryParseStepExpressionArguments(Regex regex, string stepExpression, out string[] arguments);
    }
}