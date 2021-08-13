using Gherkin.Ast;

namespace GherkinSyncTool.Interfaces
{
    public interface IFeatureFile
    {
        GherkinDocument Document { get; }
        string RelativePath { get; }
    }
}