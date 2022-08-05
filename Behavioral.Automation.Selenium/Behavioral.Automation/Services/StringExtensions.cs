using System;
using System.Linq;
using System.Text.RegularExpressions;
using Behavioral.Automation.Elements;

namespace Behavioral.Automation.Services
{
    /// <summary>
    /// Perform various operations with strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// List of element tags to avoid getting 'value' attribute in GetElementTextOrValue
        /// 'li' tag: GetAttribute("value") returns item's index, instead of it's text
        /// </summary>
        private static string[] ElementTagsToIgnore = new string[] { "li" };

        /// <summary>
        /// Parse values from the string in the given format
        /// </summary>
        /// <param name="data">String to parse</param>
        /// <param name="format">Parsing format</param>
        /// <returns>Parsed values</returns>
        public static string[] ParseExact(
            this string data,
            string format)
        {
            return ParseExact(data, format, false);
        }

        /// <summary>
        /// Parse values from the string in the given format with option to ignore case 
        /// </summary>
        /// <param name="data">String to parse</param>
        /// <param name="format">Parsing format</param>
        /// <param name="ignoreCase">Ignore case option</param>
        /// <returns>Parsed values</returns>
        public static string[] ParseExact(
            this string data,
            string format,
            bool ignoreCase)
        {
            string[] values;

            if (TryParseExact(data, format, out values, ignoreCase))
                return values;
            else
                throw new ArgumentException("Format not compatible with value.");
        }

        /// <summary>
        /// Try to parse values from string
        /// </summary>
        /// <param name="data">String to be parsed</param>
        /// <param name="format">Parsing format</param>
        /// <param name="values">Values to store parsed data</param>
        /// <returns>True if parsing was successful or false otherwise</returns>
        public static bool TryExtract(
            this string data,
            string format,
            out string[] values)
        {
            return TryParseExact(data, format, out values, false);
        }

        /// <summary>
        /// Try to parse values from string with option to ignore case
        /// </summary>
        /// <param name="data">String to be parsed</param>
        /// <param name="format">Parsing format</param>
        /// <param name="values">Values to store parsed data</param>
        /// <param name="ignoreCase">Ignore case option</param>
        /// <returns>True if parsing was successful or false otherwise</returns>
        public static bool TryParseExact(
            this string data,
            string format,
            out string[] values,
            bool ignoreCase)
        {
            int tokenCount = 0;
            format = Regex.Escape(format).Replace("\\{", "{");

            for (tokenCount = 0; ; tokenCount++)
            {
                string token = string.Format("{{{0}}}", tokenCount);
                if (!format.Contains(token)) break;
                format = format.Replace(token,
                    string.Format("(?'group{0}'.*)", tokenCount));
            }

            RegexOptions options =
                ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None;

            Match match = new Regex(format, options).Match(data);

            if (tokenCount != (match.Groups.Count - 1))
            {
                values = new string[] { };
                return false;
            }
            else
            {
                values = new string[tokenCount];
                for (int index = 0; index < tokenCount; index++)
                    values[index] =
                        match.Groups[string.Format("group{0}", index)].Value;
                return true;
            }
        }

        /// <summary>
        /// Convert string into int
        /// </summary>
        /// <param name="s">String to get number from</param>
        /// <returns>int converted from string</returns>
        public static int ParseNumberFromString(string s)
        {
            return int.Parse(Regex.Match(s, @"\d+").Value);
        }

        /// <summary>
        /// Get text or value of the web element
        /// </summary>
        /// <param name="element">Tested element</param>
        /// <returns>String with element's text or value</returns>
        public static string GetElementTextOrValue(IWebElementWrapper element)
        {
            if (ElementTagsToIgnore.Contains(element.TagName) || element.GetAttribute("value") == null)
            {
                return element.Text;
            }
            return element.GetAttribute("value");
        }
    }
}
