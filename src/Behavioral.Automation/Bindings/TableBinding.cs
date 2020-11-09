﻿using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using Behavioral.Automation.Model;
using Behavioral.Automation.Services;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Bindings
{
    [Binding]
    public sealed class TableBinding
    {
        [Given(@"(.*?) has (\d+) items")]
        [When(@"(.*?) has (\d+) items")]
        [Then(@"(.*?) should have (\d+) items")]
        public void CheckRowsCount([NotNull] ITableWrapper element, int count)
        {
            Assert.ShouldBecome(() => element.Rows.Count(), count, $"{element.Caption} has {element.Rows.Count()} rows");
        }

        [Given("(.*?) (contain|not contain) the following (rows|rows only):")]
        [Then("(.*?) should (contain|contain in exact order|not contain) the following (rows|rows only):")]
        public void CheckTableContainsRows(ITableWrapper gridRows, string behavior, string strictCondition, Table table)
        {
            var expectedValues = ListServices.TableToCellsList(table);
            bool exactOrder = behavior.Contains("contain in exact order");

            Assert.ShouldBecome(() => gridRows.Stale, false, $"{gridRows.Caption} is stale");

            bool inversion = behavior.StartsWith("not");
            if (inversion)
            {
                Assert.ShouldBecome(() => gridRows.CellsText.DoesntContainValues(expectedValues),
                    true,
                    $"{gridRows.Caption} is {gridRows.CellsText.Aggregate((x, y) => $"{x}, {y}")}");
            }
            else
            {
                if (strictCondition == "rows only")
                {
                    Assert.ShouldBecome(() => gridRows.CellsText.Count() == expectedValues.Count, true,
                        $"{gridRows.Caption} is {gridRows.CellsText.Aggregate((x, y) => $"{x}, {y}")}");
                }
                Assert.ShouldBecome(() => gridRows.CellsText.ContainsValues(expectedValues, exactOrder),
                true,
                $"{gridRows.Caption} is {gridRows.CellsText.Aggregate((x, y) => $"{x}, {y}")}");
            }
        }

        [Then("(.*?) should (contain|not contain) \"(.*)\" in (.*)")]
        public void CheckTableContainRow(ITableWrapper table, string behavior, string value, IElementCollectionWrapper column)
        {
            Assert.ShouldBecome(() => column.Stale, false, $"{column.Caption} is stale");
            Assert.ShouldBecome(() => table.Text.Contains(value), true, $"{table.Caption} text is {table.Text}");
            Assert.ShouldBecome(() => column.Elements.Any(x => x.Stale), false, 
                $"{table.Caption} elements are stale");
            ListServices.GetElementsTextsList(column.Elements).Contains(value).Should().Be(!behavior.Contains("not"));
        }

        [Then("(.*?) values should be (lesser|greater) than \"(.*)\"")]
        public void CompareTableRowGreaterLesser(IElementCollectionWrapper column,  string condition,
            int value)
        {
            Assert.ShouldBecome(() => column.Stale, false, $"{column.Caption} is stale");
            var elementsText = ListServices.GetElementsTextsList(column.Elements);
            Assert.ShouldBecome(() => ListServices.CheckListValuesLesserGreater(elementsText, condition, value), true,
                $"values are {elementsText.Aggregate((x, y) => $"{x}, {y}")}");
        }

        [Then("(.*?) values should be (between|not between) \"(.*)\" and \"(.*)\"")]
        public void CheckRowValuesAreBetween(IElementCollectionWrapper column, string behavior, int value1, int value2)
        {
            Assert.ShouldBecome(() => column.Stale, false, $"{column.Caption} is stale");
            var elementsText = ListServices.GetElementsTextsList(column.Elements);
            Assert.ShouldBecome(() => ListServices.CheckValuesAreBetween(elementsText, value1, value2), 
                !behavior.Contains("not"),
                $"values are {elementsText.Aggregate((x, y) => $"{x}, {y}")}");
        }

        [Given("(.*?) contain no records in (.*)")]
        [Then("(.*?) should contain no records in (.*)")]
        public void CheckSearchResultEmptyByColumn(ITableWrapper table, IWebElementWrapper column)
        {
            Assert.ShouldBecome(() => !column.Displayed && !table.Rows.Any(), true, $"{table.Caption} contains records");
        }

        [Given("(.*?) has no records")]
        [Then("(.*?) should contain no records")]
        public void CheckSearchResultIsEmpty(ITableWrapper table)
        {
            Assert.ShouldBecome(() => table.Displayed && !table.Rows.Any(), true, $"{table.Caption} contains records");
        }

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
