using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Services;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    /// <summary>
    /// This class stores bindings which are used to interact with table elements
    /// </summary>
    [Binding]
    public sealed class TableBinding
    {
        /// <summary>
        /// Check number of table rows
        /// </summary>
        /// <param name="element">Tested web element wrapper</param>
        /// <param name="count">Expected number of rows</param>
        /// <example>Then "Test" table should have 5 items</example>
        [Given(@"(.*?) has (\d+) items")]
        [When(@"(.*?) has (\d+) items")]
        [Then(@"(.*?) should have (\d+) items")]
        public void CheckRowsCount([NotNull] ITableWrapper element, int count)
        {
            Assert.ShouldBecome(() => element.Rows.Count(), count, $"{element.Caption} has {element.Rows.Count()} rows");
        }

        /// <summary>
        /// Check that table has expected rows
        /// </summary>
        /// <param name="gridRows">Tested web element wrapper</param>
        /// <param name="behavior">Assertion type</param>
        /// <param name="table">Specflow table which contains expected values</param>
        /// <example>
        /// Then "Test" table should contain the following rows:
        /// | column 1 | column 2 |
        /// | Test 1   | Test 2   |
        /// | Test 3   | Test 4   |
        /// </example>
        [Given("(.*?) (contain|not contain) the following rows:")]
        [Then("(.*?) should (contain|not contain) the following rows:")]
        public void CheckTableContainsRows(ITableWrapper gridRows, string behavior, Table table)
        {
            var tableList = ListServices.TableToCellsList(table);
            Assert.ShouldBecome(() => gridRows.Stale, false, $"{gridRows.Caption} is stale");
            Assert.ShouldBecome(() => gridRows.Text.Contains(tableList.FirstOrDefault()), true, $"{gridRows.Caption} text is \"{gridRows.Text}\"");
            var gridRowsCellsText = gridRows.CellsText.ToList();
            Assert.ShouldBecome(() => ListServices.CompareTwoLists(gridRows.CellsText.ToList(), tableList), !behavior.Contains("not"), 
                $"{gridRows.Caption} is {gridRowsCellsText.Aggregate((x, y) => $"{x}, {y}")}");
        }

        /// <summary>
        /// Check that one of the table's columns contains specific value
        /// </summary>
        /// <param name="table">Tested table wrapper</param>
        /// <param name="behavior">Assertion type</param>
        /// <param name="value">Expected value</param>
        /// <param name="column">Tested column with the desired value</param>
        /// <example>Then "Test" table should contain "expected string" in "Data" column</example>
        [Then("(.*?) should (contain|not contain) \"(.*)\" in (.*)")]
        public void CheckTableContainRow(ITableWrapper table, string behavior, string value, IElementCollectionWrapper column)
        {
            Assert.ShouldBecome(() => column.Stale, false, $"{column.Caption} is stale");
            Assert.ShouldBecome(() => table.Text.Contains(value), true, $"{table.Caption} text is {table.Text}");
            Assert.ShouldBecome(() => column.Elements.Any(x => x.Stale), false, 
                $"{table.Caption} elements are stale");
            ListServices.GetElementsTextsList(column.Elements).Contains(value).Should().Be(!behavior.Contains("not"));
        }

        /// <summary>
        /// Check that all numeric values in the column are lesser or greater than expected value
        /// </summary>
        /// <param name="column">Tested web element wrapper</param>
        /// <param name="condition">Lesser or greater parameter</param>
        /// <param name="value">Expected value</param>
        /// <example> Then "Test" column value should be greater than "2"</example>
        [Then("(.*?) values should be (lesser|greater) than \"(.*)\"")]
        public void CompareTableRowGreaterLesser(IElementCollectionWrapper column,  string condition,
            int value)
        {
            Assert.ShouldBecome(() => column.Stale, false, $"{column.Caption} is stale");
            var elementsText = ListServices.GetElementsTextsList(column.Elements);
            Assert.ShouldBecome(() => ListServices.CheckListValuesLesserGreater(elementsText, condition, value), true,
                $"values are {elementsText.Aggregate((x, y) => $"{x}, {y}")}");
        }

        /// <summary>
        /// Check that all numeric values in the column are between given boundaries
        /// </summary>
        /// <param name="column">Tested web element wrapper</param>
        /// <param name="behavior">Assertion type</param>
        /// <param name="value1">First boundary</param>
        /// <param name="value2">Second boundary</param>
        /// <example>Then "Test" column values should be between "5" and "10"</example>
        [Then("(.*?) values should be (between|not between) \"(.*)\" and \"(.*)\"")]
        public void CheckRowValuesAreBetween(IElementCollectionWrapper column, string behavior, int value1, int value2)
        {
            Assert.ShouldBecome(() => column.Stale, false, $"{column.Caption} is stale");
            var elementsText = ListServices.GetElementsTextsList(column.Elements);
            Assert.ShouldBecome(() => ListServices.CheckValuesAreBetween(elementsText, value1, value2), 
                !behavior.Contains("not"),
                $"values are {elementsText.Aggregate((x, y) => $"{x}, {y}")}");
        }

        /// <summary>
        /// Check that one of the table's columns has no data
        /// </summary>
        /// <param name="table">Tested table wrapper</param>
        /// <param name="column">Tested column wrapper</param>
        /// <example>Then "Test" table should contain no records in "Data" column</example>
        [Given("(.*?) contain no records in (.*)")]
        [Then("(.*?) should contain no records in (.*)")]
        public void CheckSearchResultEmptyByColumn(ITableWrapper table, IWebElementWrapper column)
        {
            Assert.ShouldBecome(() => !column.Displayed && !table.Rows.Any(), true, $"{table.Caption} contains records");
        }

        /// <summary>
        /// Check that table has no data
        /// </summary>
        /// <param name="table">Tested web element wrapper</param>
        /// <example>Then "Test" table should contain no records</example>
        [Given("(.*?) has no records")]
        [Then("(.*?) should contain no records")]
        public void CheckSearchResultIsEmpty(ITableWrapper table)
        {
            Assert.ShouldBecome(() => table.Displayed && !table.Rows.Any(), true, $"{table.Caption} contains records");
        }

        /// <summary>
        /// Check that one of the table's rows is expanded
        /// </summary>
        /// <param name="index">Number of row</param>
        /// <param name="table">Tested web element wrapper</param>
        /// <param name="behavior">Assertion type</param>
        /// <example>Then "first" element among "Test" table rows should be expanded</example>
        [Given("(.*?) element among (.*) (is| is not) expanded")]
        [When("(.*?) element among (.*) (is| is not) expanded")]
        [Then("(.*?) element among (.*) should (be|be not) expanded")]
        public void CheckRowIsExpanded(int index, IElementCollectionWrapper table, string behavior)
        {
            var check = table.GetAttributeByIndex("class", index-1).Contains("expandable-expanded-row");
            check.Should().Be(!behavior.Contains("not"));
        }
    }
}
