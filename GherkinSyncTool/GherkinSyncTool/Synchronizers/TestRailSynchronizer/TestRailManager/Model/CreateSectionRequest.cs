namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model
{
    public class CreateSectionRequest
    {
        public ulong ProjectId { get; init; }
        public string Description { get; init; }
        public ulong? ParentId { get; init; }
        public ulong SuiteId { get; init; }
        public string Name { get; init; }  
    }
}