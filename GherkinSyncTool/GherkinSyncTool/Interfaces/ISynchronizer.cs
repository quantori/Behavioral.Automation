using System.Collections.Generic;

namespace GherkinSyncTool.Interfaces
{
    public interface ISynchronizer
    {
        void Sync(List<IFeatureFile> featureFiles);
    }
}