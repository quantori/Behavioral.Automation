using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Behavioral.Automation.Elements;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class InputBinding
    {
        private readonly ITestRunner _runner;

        public InputBinding([NotNull] ITestRunner runner)
        {
            _runner = runner;
        }

        [When("user enters \"(.*)\" into (.*)")]
        public void EnterInput([NotNull] string input, [NotNull] ITextElementWrapper element)
        {
            element.EnterString(input);
        }

        [Given("user clears (.*)")]
        [When("user clears (.*)")]
        public void ClearInput([NotNull] ITextElementWrapper element)
        {
            element.ClearInput();
        }

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