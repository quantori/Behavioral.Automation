using System.IO;
using Gherkin.Ast;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Interfaces;

namespace GherkinSyncTool.Models
{
    public class FeatureFile : IFeatureFile
    {
        public GherkinDocument Document { get; init; }

        private string _absolutePath;
        private string _relativePath;

        public string AbsolutePath
        {
            get => _absolutePath;
            set => _absolutePath = Path.GetFullPath(value);
        }

        public string RelativePath
        {
            get => Path.GetRelativePath(
                    Directory.GetParent(ConfigurationManager.GetConfiguration().BaseDirectory).FullName,
                    AbsolutePath);
        }
    }
}