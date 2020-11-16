using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
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

        [Given("(.+?) (has|does not have) the following items:")]
        [Then("(.+?) should (have|not have) the following items:")]
        public void CheckListHaveItems(IListWrapper list, string behavior,  Table table)
        {
            var expectedListValues = ListServices.TableToRowsList(table);
            bool checkResult;
            if (behavior.Contains("not"))
            {
                checkResult = list.ListValues.DoesntContainValues(expectedListValues);
            }
            else
            {
                Assert.ShouldBecome(() => list.ListValues.Count() == expectedListValues.Count, true,
                    $"{list.Caption} has {list.ListValues.Count()} elements");

                checkResult = list.ListValues.HaveValues(expectedListValues, false);
            }

            checkResult.Should().Be(true);
        }

        [Given("(.+?) (contains|does not contain) the following items:")]
        [Then("(.+?) should (contain|not contain) the following items:")]
        public void CheckListContainsItems(IListWrapper list, string behavior, Table table)
        {
            var expectedListValues = ListServices.TableToRowsList(table);
            bool checkResult;
            if (behavior.Contains("not"))
            {
                checkResult = list.ListValues.DoesntContainValues(expectedListValues);
            }
            else
            {
                checkResult = list.ListValues.ContainsValues(expectedListValues);
            }

            checkResult.Should().Be(true);
        }

        [Then("(.+?) should have in exact order the following items:")]
        public void CheckListContainsItemsInExactOrder(IListWrapper list,  Table table)
        {
            var expectedListValues = ListServices.TableToRowsList(table);
            var checkResult = list.ListValues.HaveValues(expectedListValues, true);
            checkResult.Should().Be(true);
        }
    }
}
