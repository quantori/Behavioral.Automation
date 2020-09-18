using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// This class stores bindings used to interact with list elements
    /// </summary>
    [Binding]
    public class ListBinding
    {
        private readonly IDriverService _driverService;

        public ListBinding([NotNull] IDriverService driverService)
        {
            _driverService = driverService;
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
        [Given("(.*?) (contain|not contain) the following items:")]
        [Then("(.*?) should (contain|not contain) the following items:")]
        public void CheckListContainsItems(IListWrapper list, string behavior, Table table)
        {
            bool check = 
                ListServices.CheckListContainValuesFromAnotherList(list.ListValues.ToList(), ListServices.TableToRowsList(table));
            check.Should().Be(!behavior.Contains("Not"));
        }

    }
}
