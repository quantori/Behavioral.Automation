using Gherkin.Ast;
using GherkinSyncTool.Interfaces;

namespace GherkinSyncTool.Models
{
    public class FeatureFile : IFeatureFile
    {
        public GherkinDocument Document { get; init; }
        public string RelativePath { get; init;  }
    }
}