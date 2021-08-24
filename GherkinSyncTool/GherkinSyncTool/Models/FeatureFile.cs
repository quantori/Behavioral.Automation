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

            var env = Environment.OSVersion.Platform;
            
            Document = document;
            AbsolutePath = Path.GetFullPath(path);
            
            var baseDirectory = new DirectoryInfo(ConfigurationManager.GetConfiguration().BaseDirectory);
            Log.Debug($"");
            Log.Debug($"");
            var relativeTo = baseDirectory.Parent.FullName;
            Log.Debug($"Document: {Document.Feature.Name} " +
                      $" - Base directory: {baseDirectory}" +
                      $" - Relative to directory: {relativeTo}");
            RelativePath = Path.GetRelativePath(relativeTo, 
                AbsolutePath);
        }

        public string AbsolutePath { get; }
        public string RelativePath { get; }
    }
}