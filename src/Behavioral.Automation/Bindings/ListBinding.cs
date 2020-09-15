using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Services;
using JetBrains.Annotations;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public class ListBinding
    {
        private readonly IDriverService _driverService;

        public ListBinding([NotNull] IDriverService driverService)
        {
            _driverService = driverService;
        }

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
