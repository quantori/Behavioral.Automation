using System.Collections.Generic;
using System.Data;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Elements.Interfaces;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Services
{
    public static class ListServices
    {
        public static List<string> GetElementsTextsList(IEnumerable<IWebElementWrapper> elements)
        {
            return elements.Select(item => item.Text.Replace("\r\n", " ")).ToList();
        }

        public static List<string> ExcelToCellsList(DataSet set, int rowsCount, int columnsCount)
        {
            List<string> result = new List<string>();
            for (int row = 1; row <= rowsCount; row++)
            {
                for (int col = 0; col < columnsCount; col++)
                {
                    result.Add(set.Tables[0].Rows[row][col].ToString());
                }
            }
            return result;
        }

        public static List<string> TableToCellsList(Table table)
        {
            List<string> result = new List<string>();
            foreach (var row in table.Rows)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    result.Add(row[i]);
                }
            }
            return result;
        }

        public static List<int> StringListToInt(List<string> stringList)
        {
            return stringList.ConvertAll(int.Parse);
        }

        public static bool CheckListValuesLesserGreater(List<string> list, string condition, int value)
        {
            List<int> intList = StringListToInt(list);
            if (condition.Equals("greater"))
            {
                return intList.All(x => x >= value);
            }
            return intList.All(x => x <= value);
        }

        public static bool CheckValuesAreBetween(List<string> list, int value1, int value2)
        {
            List<int> intList = StringListToInt(list);
            return intList.All(x => x >= value1 && x <= value2);
        }

        public static IEnumerable<StringRow> ToStringRows(this IEnumerable<ITableRowWrapper> actualCollection)
        {
            return actualCollection.Select(x => new StringRow(x.CellsText.ToArray()));
        }

        public static StringRow[] ToStringRows(this TableRows expectedValues)
        {
            return expectedValues.Select(x => new StringRow(x.Values.ToArray())).ToArray();
        }

        public static bool HaveValues<T>(this IEnumerable<T> actualCollection, T[] expectedValues, bool exactOrder)
        {
            if (exactOrder)
            {
                return actualCollection.SequenceEqual(expectedValues);
            }

            List<T> expectedRowsToCheck = new List<T>(expectedValues);
            foreach (var actualRow in actualCollection)
            {
                var equivalentRow = expectedRowsToCheck.FirstOrDefault(x => x.Equals(actualRow));
                if (equivalentRow == null)
                {
                    return false;
                }
                expectedRowsToCheck.Remove(equivalentRow);
            }

            return !expectedRowsToCheck.Any();
        }

        public static bool ContainsValues<T>(this IEnumerable<T> actualCollection, T[] expectedValues)
        {
            var expectedRows = new HashSet<T>(expectedValues);
            if (expectedRows.IsSubsetOf(actualCollection))
            {
                return true;
            }
            return false;
        }

        public static bool DoesntContainValues<T>(this IEnumerable<T> actualCollection,
            T[] expectedValues)
        {
            return !actualCollection.Intersect(expectedValues).ToList().Any();
        }
    }
}
