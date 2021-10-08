using System;
using System.Text.RegularExpressions;
using Behavioral.Automation.Elements;
using Behavioral.Automation.Elements.Interfaces;

namespace Behavioral.Automation.Services
{
    public static class StringExtensions
    {
        public static string[] ParseExact(
            this string data,
            string format)
        {
            return ParseExact(data, format, false);
        }

        public static string[] ParseExact(
            this string data,
            string format,
            bool ignoreCase)
        {
            if (TryParseExact(data, format, out var values, ignoreCase))
                return values;
            else
                throw new ArgumentException("Format not compatible with value.");
        }

        public static bool TryExtract(
            this string data,
            string format,
            out string[] values)
        {
            return TryParseExact(data, format, out values, false);
        }

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

        public static int ParseNumberFromString(string s)
        {
            return int.Parse(Regex.Match(s, @"\d+").Value);
        }

        public static string GetElementTextOrValue(IWebElementWrapper element)
        {
            if (element.GetAttribute("value") == null)
            {
                return element.Text;
            }
            return element.GetAttribute("value");
        }
    }
}
