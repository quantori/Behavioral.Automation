namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.Model
{
    public class CreateCaseRequest
    {
        public ulong SectionId { get; init; }
        public string Title { get; init; }
        public ulong? TypeId { get; } = null;
        public ulong? PriorityId { get; } = null;
        public string Estimate { get; } = null;
        public ulong? MilestoneId { get; } = null;
        public string Refs { get; } = null;
        public CaseCustomFields CustomFields { get; init; }
        public ulong? TemplateId { get; init; }
    }
}