using System;
using System.IO;
using System.Reflection;
using Gherkin.Ast;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Interfaces;
using NLog;

namespace GherkinSyncTool.Models
{
    public class FeatureFile : IFeatureFile
    {
        public GherkinDocument Document { get; init; }
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);


        public FeatureFile(GherkinDocument document, string path)
        {
            if (!new FileInfo(path).Exists)
                throw new DirectoryNotFoundException($"File {path} not found");

            Document = document;
            AbsolutePath = Path.GetFullPath(path);
            
            var baseDirectory = new DirectoryInfo(ConfigurationManager.GetConfiguration().BaseDirectory);
            var relativeToDirectory = baseDirectory?.Parent?.FullName ?? 
                                      throw new FileNotFoundException($"Base directory {baseDirectory} does not have a parent. Feature file creation is not possible");
            RelativePath = Path.GetRelativePath(relativeToDirectory, AbsolutePath);
            Log.Debug($"{Environment.NewLine}Parsing document \"{Document.Feature.Name}\"..." +
                      $"{Environment.NewLine} - Base directory: {baseDirectory}" +
                      $"{Environment.NewLine} - Relative to directory: {relativeToDirectory} " +
                      $"{Environment.NewLine} - Relative path result: {RelativePath}");
        }

        public string AbsolutePath { get; }
        public string RelativePath { get; }
    }
}