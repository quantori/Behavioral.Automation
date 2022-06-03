using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Model;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Services
{
    public class StepParametersProcessor : IStepParametersProcessor
    {
        private readonly Regex _stepParameterRegex = new Regex(@"(\((?!\?\:).*?\))");
        private readonly ScenarioContext _scenarioContext;

        public StepParametersProcessor(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public string CreateStepExpression(string pattern, params object[] arguments)
        {
            MatchCollection g = _stepParameterRegex.Matches(pattern);
            Queue<object> argumentsToProcessQueue = new Queue<object>(arguments);
            if (g.Count != arguments.Length)
            {
                return "";
            }

            return Regex.Replace(pattern, @"(\(.*?\))",
                match => ConvertValueToString(argumentsToProcessQueue.Dequeue()));
        }

        public bool TryParseStepExpressionArguments(Regex regex, string stepExpression, out string[] arguments)
        {
            MatchCollection g = regex.Matches(stepExpression);
            if (g.Count > 0)
            {
                arguments = g[0].Groups.Values.Skip(1).Select(v => v.Value).ToArray();
                return true;
            }


            arguments = new string[] { };
            return false;
        }

        private string ConvertValueToString(object o)
        {
            switch (o)
            {
                case { } obj when IsNumeric(o):
                    return obj.ToString();
                case string s:
                    return s;
                case IWebElementWrapper w:
                    return w.Caption;
                case AssertionBehavior b:
                    return BuildBehaviourString(b);
                default:
                    throw new ArgumentException($"Provided value with type {o.GetType()} cannot be passed to step");
            }
        }

        private bool IsNumeric(object expression)
        {
            if (expression == null)
                return false;

            return double.TryParse(Convert.ToString(expression
                    , CultureInfo.InvariantCulture)
                , NumberStyles.Any
                , NumberFormatInfo.InvariantInfo
                , out _);
        }

        private string BuildBehaviourString(AssertionBehavior b)
        {
            var currentStepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType;
            string behaviorString = string.Empty;
            if (b.Type == AssertionType.Immediate)
            {
                switch (currentStepType)
                {
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Given:
                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.When:
                        behaviorString = "is";
                        break;

                    case TechTalk.SpecFlow.Bindings.StepDefinitionType.Then:
                        behaviorString = "be";
                        break;
                }
            }
            else
            {
                behaviorString = "become";
            }

            if (b.Inversion)
            {
                behaviorString += " not";
            }

            return behaviorString;
        }

    }
}