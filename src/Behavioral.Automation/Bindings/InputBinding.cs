using System;
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

        [Given("user entered \"(.+?)\" into (.+?)")]
        [When("user enters \"(.+?)\" into (.+?)")]
        public void EnterInput([NotNull] string input, [NotNull] ITextElementWrapper element)
        {
            var stringToReplace = "__random_string";
            if (input.Contains(stringToReplace))
            {
                var length = 8;
                
                if (input.Split(':').Length > 1)
                {
                    var strNumber = input.Split(':')[1];
                    length = Int32.Parse(strNumber);
                    stringToReplace += ":" + strNumber;
                };
                var randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, length);

                input = input.Replace(stringToReplace, randomString);
            }
            element.EnterString(input);
        }

        [Given("user cleared (.*)")]
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