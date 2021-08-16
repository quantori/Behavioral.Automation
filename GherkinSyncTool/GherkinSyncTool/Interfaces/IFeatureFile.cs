using Gherkin.Ast;

namespace GherkinSyncTool.Interfaces
{
    public interface IFeatureFile
    {
        GherkinDocument Document { get; }
        string AbsolutePath { get; }
        string RelativePath { get; }
    }
}