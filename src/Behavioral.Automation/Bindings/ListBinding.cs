using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Elements.Interfaces;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public class ListBinding
    {

        [Given("(.+?) (has|does not have) the following items:")]
        [Then("(.+?) should (have|not have) the following items:")]
        public void CheckListHaveItems(IListWrapper list, string behavior, Table table)
        {
            bool checkResult;
            var expectedValues = table.Rows.Select(r => r.Values.First()).ToArray();
            if (behavior.Contains("not"))
            {
                
                checkResult = list.ListValues.DoesntContainValues(expectedValues);
            }
            else
            {
                checkResult = list.ListValues.HaveValues(expectedValues, false);
            }

            checkResult.Should().Be(true);
        }

        [Given("(.+?) (contains|does not contain) the following items:")]
        [Then("(.+?) should (contain|not contain) the following items:")]
        public void CheckListContainsItems(IListWrapper list, string behavior, Table table)
        {
            bool checkResult;
            var expectedValues = table.Rows.Select(r => r.Values.First()).ToArray();
            if (behavior.Contains("not"))
            {
                checkResult = list.ListValues.DoesntContainValues(expectedValues);
            }
            else
            {
                checkResult = list.ListValues.ContainsValues(expectedValues);
            }

            checkResult.Should().Be(true);
        }

        [Then("(.+?) should have in exact order the following items:")]
        public void CheckListContainsItemsInExactOrder(IListWrapper list, Table table)
        {
            var expectedValues = table.Rows.Select(r => r.Values.First()).ToArray();
            var checkResult = list.ListValues.HaveValues(expectedValues, true);
            checkResult.Should().Be(true);
        }
    }
}
