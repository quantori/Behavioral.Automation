using System.Text.RegularExpressions;

namespace Behavioral.Automation.Playwright.Utils;

public static class StringExtensions
{
    public static string ToCamelCase(this string str)
    {
        return Regex.Replace(str, @"[ ](\w)", m => m.Groups[1].Value.ToUpper());
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
}