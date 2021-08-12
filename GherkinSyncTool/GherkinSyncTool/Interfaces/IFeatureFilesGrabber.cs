using System.Collections.Generic;

namespace GherkinSyncTool.Interfaces
{
    public interface IFeatureFilesGrabber
    {
        List<IFeatureFile> TakeFiles(string sourceDirectoryPath);
    }
}