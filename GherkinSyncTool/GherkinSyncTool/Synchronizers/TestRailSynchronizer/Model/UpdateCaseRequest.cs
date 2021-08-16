namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.Model
{
    public class UpdateCaseRequest
    {
        public ulong CaseId { get; init; }
        public string Title { get; init; }
        public ulong? TypeId { get; } = null;
        public ulong? PriorityId { get; } = null;
        public string Estimate { get; } = null;
        public ulong? MilestoneId { get; } = null;
        public string Refs { get; } = null;
        public CaseCustomFields CustomFields { get; set; } = null;
    }
}