using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Services;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// Bindings for lists testing
    /// </summary>
    [Binding]
    public class ListBinding
    {
        /// <summary>
        /// Check that list has items given in the Specflow table
        /// </summary>
        /// <param name="list">List web element wrapper</param>
        /// <param name="behavior">Assertion type</param>
        /// <param name="table">Specflow table which contains expected values</param>
        /// <example>
        /// Then "Test" list should have the following items:
        /// | itemName     |
        /// | Test value 1 |
        /// | Test value 2 |
        /// </example>
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

        /// <summary>
        /// Check that list contains items given in the Specflow table
        /// </summary>
        /// <param name="list">List web element wrapper</param>
        /// <param name="behavior">Assertion type</param>
        /// <param name="table">Specflow table which contains expected values</param>
        /// <example>
        /// Then "Test" list should contain the following items:
        /// | itemName     |
        /// | Test value 1 |
        /// | Test value 2 |
        /// </example>
        [Given("the (.+?) (contains|does not contain) the following items:")]
        [Then("the (.+?) should (contain|not contain) the following items:")]
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
                checkResult = list.ListValues.ContainsValues(expectedValues, false);
            }

            checkResult.Should().Be(true);
        }

        /// <summary>
        /// Check that list has items given in the Specflow table in exact order
        /// </summary>
        /// <param name="list">List web element wrapper</param>
        /// <param name="behavior">Assertion type</param>
        /// <param name="table">Specflow table which contains expected values</param>
        /// <example>
        /// Then "Test" list should have in exact order the following items:
        /// | itemName     |
        /// | Test value 1 |
        /// | Test value 2 |
        /// </example>
        [Given("the (.+?) has in exact order the following items:")]
        [Then("the (.+?) should have in exact order the following items:")]
        public void CheckListContainsItemsInExactOrder(IListWrapper list, Table table)
        {
            var expectedValues = table.Rows.Select(r => r.Values.First()).ToArray();
            var checkResult = list.ListValues.HaveValues(expectedValues, true);
            checkResult.Should().Be(true);
        }
    }
}
