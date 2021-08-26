using System.IO;
using System.Linq;

namespace GherkinSyncTool.Utils
{
    internal static class HelperMethods
    {
        internal static void InsertLineToTheFile(string path, int lineNumber, string text)
        {
            var featureFIleLines = File.ReadAllLines(path).ToList();
            featureFIleLines.Insert(lineNumber, text);
            File.WriteAllLines(path, featureFIleLines);
        }

        internal static void ReplaceLineInTheFile(string path, string oldLine, string newLine)
        {
            var featureFileLines = File.ReadAllLines(path).ToList();
            var index = featureFileLines.FindIndex(s=>s.Contains(oldLine));
            if(index >= 0)
            {
                featureFileLines[index] = featureFileLines[index].Replace(oldLine, newLine);
                File.WriteAllLines(path, featureFileLines);
            }
        }
    }
}