namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model
{
    public class CreateSectionRequest
    {
        public ulong ProjectId { get; set; }
        public string Description { get; set; }
        public ulong? ParentId { get; set; }
        public ulong SuiteId { get; set; }
        public string Name { get; set; }  
    }
}