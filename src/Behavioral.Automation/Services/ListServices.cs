using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Behavioral.Automation.Elements;
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

        public static List<string> TableToRowsList(Table table)
        {
            List<string> result = new List<string>();
            foreach (var row in table.Rows)
            {
                string rowText = string.Empty;
                for (int i = 0; i < row.Count; i++)
                {
                    if (row[i] != string.Empty)
                    {
                        rowText += row[i] + " ";
                    }
                }
                result.Add(rowText.Remove(rowText.Length - 1, 1));
            }
            return result;
        }

        public static bool CheckListContainValue(List<string> list, string value)
        {
           return list.Any(s => s == value);
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

        public static bool CompareTwoLists(List<string> list1, List<string> list2)
        {
            return list1.All(item => list2.Contains(item)) &&
                   list2.All(item => list1.Contains(item));
        }

        public static bool ContainsValues(this IEnumerable<string> actualCollection, List<string> expectedValues, bool exactOrder)
        {
            int index = 0;
            int maxIndexForValuesList = expectedValues.Count - 1;

            foreach (var value in actualCollection)
            {
                if (index > maxIndexForValuesList)
                {
                    break;
                }
                bool collectionContainsValue = exactOrder ?
                    expectedValues[index].Equals(value, StringComparison.Ordinal)
                    : expectedValues.Contains(value, StringComparer.Ordinal);
                if (!collectionContainsValue)
                {
                    return false;
                }
                if (index == maxIndexForValuesList)
                {
                    return true;
                }
                index++;
            }
            return false;
        }
    }
}
