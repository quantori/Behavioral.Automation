using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Contains BuildAction methods which should be used to call other bindings inside the code (multiple clicks, value selections, etc.)
    /// This way actions in called bindings will appear in test logs
    /// </summary>
    public class ComplexBindingBuilder : IComplexBindingBuilder
    {
        private readonly ITestRunner _runner;
        private readonly IStepParametersProcessor _stepParametersProcessor;
        private readonly ScenarioContext _scenarioContext;

        private readonly int _indentationSize = 4;
        private int _indendationLevel = 1;

        public ComplexBindingBuilder(
            ITestRunner runner,
            IStepParametersProcessor stepParametersProcessor,
            ScenarioContext scenarioContext)
        {
            _runner = runner;
            _stepParametersProcessor = stepParametersProcessor;
            _scenarioContext = scenarioContext;
        }

        public void Indent()
        {
            _indendationLevel++;
        }

        public void ReduceIndentation()
        {
            _indendationLevel--;
        }

        public void BuildAction(Action method)
        {
            var attributes = method.GetMethodInfo().GetCustomAttributes<StepDefinitionBaseAttribute>(true);
            BuildAction(Array.Empty<object>(), attributes, method.GetMethodInfo().Name);
        }

        public void BuildAction<T>(Action<T> method, params object[] pars)
        {
            var attributes = method.GetMethodInfo().GetCustomAttributes<StepDefinitionBaseAttribute>(true);
            BuildAction(pars, attributes, method.GetMethodInfo().Name);
        }

        public void BuildAction<T1, T2>(Action<T1, T2> method, params object[] pars)
        {
            var attributes = method.GetMethodInfo().GetCustomAttributes<StepDefinitionBaseAttribute>(true);
            BuildAction(pars, attributes, method.GetMethodInfo().Name);
        }

        public void BuildAction<T1, T2, T3>(Action<T1, T2, T3> method, params object[] pars)
        {
            var attributes = method.GetMethodInfo().GetCustomAttributes<StepDefinitionBaseAttribute>(true);
            BuildAction(pars, attributes, method.GetMethodInfo().Name);
        }

        public void BuildAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method, params object[] pars)
        {
            var attributes = method.GetMethodInfo().GetCustomAttributes<StepDefinitionBaseAttribute>(true);
            BuildAction(pars, attributes, method.GetMethodInfo().Name);
        }

        private void BuildAction(object[] pars, IEnumerable<StepDefinitionBaseAttribute> attributes, string methodName)
        {
            var definitionType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var attributeForCurrentStep = attributes.FirstOrDefault(a => a.GetType().Name.StartsWith(definitionType, StringComparison.OrdinalIgnoreCase));
            if (attributeForCurrentStep == null)
            {
                Assert.Inconclusive($"{definitionType} attribute is not provided for {methodName} binding method");
            }

            string stepExpression;
            var table = pars.FirstOrDefault(d => d.GetType() == typeof(Table));
            if (table != null)
            {
                var parsList = pars.ToList();
                parsList.Remove(table);
                stepExpression = _stepParametersProcessor.CreateStepExpression(attributeForCurrentStep.Regex, parsList.ToArray());
                RunAction(stepExpression, (Table)table);
            }
            else
            {
                stepExpression = _stepParametersProcessor.CreateStepExpression(attributeForCurrentStep.Regex, pars);
                RunAction(stepExpression);
            }
            TestContext.WriteLine($"{new String(' ', _indendationLevel * _indentationSize)}{stepExpression}");
        }

        private void RunAction(string stepExpression)
        {
            switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    _runner.Given(stepExpression);
                    break;

                case StepDefinitionType.When:
                    _runner.When(stepExpression);
                    break;

                case StepDefinitionType.Then:
                    _runner.Then(stepExpression);
                    break;
            }
        }

        private void RunAction(string stepExpression, Table table)
        {
            switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case StepDefinitionType.Given:
                    _runner.Given(stepExpression, null, table);
                    break;

                case StepDefinitionType.When:
                    _runner.When(stepExpression, null, table);
                    break;

                case StepDefinitionType.Then:
                    _runner.Then(stepExpression, null, table);
                    break;
            }
        }
    }
}
