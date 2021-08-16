using System.IO;
using Gherkin.Ast;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Interfaces;

namespace GherkinSyncTool.Models
{
    public class FeatureFile : IFeatureFile
    {
        public GherkinDocument Document { get; init; }
        public string AbsolutePath { get; init;  }

        public string RelativePath
        {
            get => Path.GetRelativePath(Directory.GetParent(ConfigurationManager.GetConfiguration().BaseDirectory).FullName, AbsolutePath);
        }
    }
}