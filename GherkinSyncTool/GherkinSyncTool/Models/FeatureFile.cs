using System.IO;
using Gherkin.Ast;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Interfaces;

namespace GherkinSyncTool.Models
{
    public class FeatureFile : IFeatureFile
    {
        public GherkinDocument Document { get; init; }

        public FeatureFile(GherkinDocument document, string path)
        {
            if (!new FileInfo(path).Exists)
                throw new DirectoryNotFoundException($"File {path} not found");
            
            Document = document;
            AbsolutePath = Path.GetFullPath(path);
            RelativePath = Path.GetRelativePath(
                Directory.GetParent(ConfigurationManager.GetConfiguration().BaseDirectory).FullName, 
                AbsolutePath);
        }

        public string AbsolutePath { get; }
        public string RelativePath { get; }
    }
}