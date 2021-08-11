using Newtonsoft.Json;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model
{
    public class CustomStepsSeparated
    {
        [JsonProperty("content")] 
        public string Content { get; init; }
    }
}