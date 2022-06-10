using System.Text.RegularExpressions;

namespace Behavioral.Automation.Playwright.Utils
{
    /// <summary>
    /// Perform various operations with strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert string into int
        /// </summary>
        /// <param name="s">String to get number from</param>
        /// <returns>int converted from string</returns>
        public static int ParseNumberFromString(string s)
        {
            return int.Parse(Regex.Match(s, @"\d+").Value);
        }
    }
}
