using System.Collections.Generic;
using System.Data;
using System.Linq;
using Behavioral.Automation.Elements;
using Behavioral.Automation.FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Methods for Lists operations
    /// </summary>
    public static class ListServices
    {
        /// <summary>
        /// Get multiple elements' texts as a list of strings
        /// </summary>
        /// <param name="elements">IEnumerable of Web element wrappers</param>
        /// <returns>List of elements' texts</returns>
        public static List<string> GetElementsTextsList(IEnumerable<IWebElementWrapper> elements)
        {
            return elements.Select(item => item.Text.Replace("\r\n", " ")).ToList();
        }


        /// <summary>
        /// Convert Excel DataSet into list of strings
        /// </summary>
        /// <param name="set">Excel cells dataset</param>
        /// <param name="rowsCount">Number of rows with tested data</param>
        /// <param name="columnsCount">Number of columns with tested data</param>
        /// <returns>List with Excel cells texts</returns>
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

        /// <summary>
        /// Convert Specflow table cells into list of strings
        /// </summary>
        /// <param name="table">Specflow table</param>
        /// <returns>List of table cells texts</returns>
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

        /// <summary>
        /// Convert Specflow table rows into list of strings
        /// </summary>
        /// <param name="table">Specflwo table</param>
        /// <returns>List of table rows texts</returns>
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

        /// <summary>
        /// Check that list contains specific value
        /// </summary>
        /// <param name="list">Tested list</param>
        /// <param name="value">Value that list should contain</param>
        /// <returns>True if list contains value or false if not</returns>
        public static bool CheckListContainValue(List<string> list, string value)
        {
           return list.Any(s => s == value);
        }

        /// <summary>
        /// Convert list of strings into list of integers
        /// </summary>
        /// <param name="stringList">List of strings</param>
        /// <returns>List of integers</returns>
        public static List<int> StringListToInt(List<string> stringList)
        {
            return stringList.ConvertAll(int.Parse);
        }

        /// <summary>
        /// Check that all numeric values in the list are greater or lesser than stated value
        /// </summary>
        /// <param name="list">List with tested values</param>
        /// <param name="condition">Test condition (greater or lesser)</param>
        /// <param name="value">Value to compare list to</param>
        /// <returns>True if check passes or false otherwise</returns>
        public static bool CheckListValuesLesserGreater(List<string> list, string condition, int value)
        {
            List<int> intList = StringListToInt(list);
            if (condition.Equals("greater"))
            {
                return intList.All(x => x >= value);
            }
            return intList.All(x => x <= value);
        }

        /// <summary>
        /// Check that numeric values inside the list are between given boundaries 
        /// </summary>
        /// <param name="list">List with tested values</param>
        /// <param name="value1">Lower boundary</param>
        /// <param name="value2">Upper boundary</param>
        /// <returns>True if check passes or false otherwise</returns>
        public static bool CheckValuesAreBetween(List<string> list, int value1, int value2)
        {
            List<int> intList = StringListToInt(list);
            return intList.All(x => x >= value1 && x <= value2);
        }

        /// <summary>
        /// Check that two lists have the same values
        /// </summary>
        /// <param name="list1">First list</param>
        /// <param name="list2">Second list</param>
        /// <returns>True if check passes or false otherwise</returns>
        public static bool CompareTwoLists(List<string> list1, List<string> list2)
        {
            return list1.All(item => list2.Contains(item)) &&
                   list2.All(item => list1.Contains(item));
        }

        /// <summary>
        /// Check that one list contains values from the other in exact order
        /// </summary>
        /// <param name="checkingList">List to be checked</param>
        /// <param name="refList">List with reference values</param>
        /// <returns>True if check passes or false otherwise</returns>
        public static bool CheckListContainValuesFromAnotherListInExactOrder(List<string> checkingList, List<string> refList)
        {
            if (refList.Count > checkingList.Count)
            {
                return false;
            }
            for (int i = 0; i < refList.Count; i++)
            {
                if (checkingList[i] != refList[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check that two lists have intersecting values
        /// </summary>
        /// <param name="list1">First list</param>
        /// <param name="list2">Second list</param>
        /// <returns>True if check passes or false otherwise</returns>
        public static bool CheckListContainValuesFromAnotherList(List<string> list1, List<string> list2)
        {
            return list1.Intersect(list2).Any();
        }
    }
}
