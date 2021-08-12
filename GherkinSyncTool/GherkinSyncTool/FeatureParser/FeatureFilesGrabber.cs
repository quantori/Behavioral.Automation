using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GherkinSyncTool.Interfaces;
using NLog;

namespace GherkinSyncTool.FeatureParser
{
    public class FeatureFilesGrabber : IFeatureFilesGrabber
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly FeatureParser _featureParser;

        public FeatureFilesGrabber(FeatureParser parser)
        {
            _featureParser = parser;
        }

        public List<IFeatureFile> TakeFiles(string sourceDirectoryPath)
        {
            var gherkinFilePaths = Directory.EnumerateFiles(sourceDirectoryPath, "*.feature",
                SearchOption.AllDirectories);
            Log.Info($"# Scanning for feature files in {sourceDirectoryPath}");
            return _featureParser.Parse(gherkinFilePaths);
        }
    }
}