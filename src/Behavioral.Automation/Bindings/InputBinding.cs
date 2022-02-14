using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Elements;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings for inputs or text fields testing
    /// </summary>
    [Binding]
    public sealed class InputBinding
    {
        private readonly ITestRunner _runner;

        public InputBinding([NotNull] ITestRunner runner)
        {
            _runner = runner;
        }

        /// <summary>
        /// Enter string into text field or input
        /// </summary>
        /// <param name="input">String to enter</param>
        /// <param name="element">Tested web element wrapper</param>
        /// <example>When user enters "test string" into "Test" input</example>
        [Given("user entered \"(.+?)\" into (.+?)")]
        [When("user enters \"(.+?)\" into (.+?)")]
        public void EnterInput([NotNull] string input, [NotNull] ITextElementWrapper element)
        { 
            element.EnterString(input);
        }

        /// <summary>
        /// Clear input or text field
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        [Given("user cleared (.*)")]
        [When("user clears (.*)")]
        public void ClearInput([NotNull] ITextElementWrapper element)
        {
            element.ClearInput();
        }

        /// <summary>
        /// Enter multiple value into multiple controls
        /// </summary>
        /// <param name="table">Specflow table which stores name of controls and values to enter</param>
        /// <example>
        /// When user enters the following values into the following controls:
        /// | value        | controlName | controlType |
        /// | "Test value" | Test1       | input       |
        /// | "Test value" | Test2       | input       |
        /// </example>
        [Given("user entered the following values into the following controls:")]
        [When("user enters the following values into the following controls:")]
        public void EnterInputIntoMultipleControls([NotNull] Table table)
        {
            foreach (var row in table.Rows)
            {
                var control = "\"" + row[1] + "\" " + row.Values.Last();
                _runner.When($"user enters {row.Values.First()} into {control}");
            }
        }
    }
}