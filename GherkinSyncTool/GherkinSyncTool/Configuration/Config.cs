using System.IO;

namespace GherkinSyncTool.Configuration
{
    public class Config
    {
        public string TagIdPattern { get; set; } = "tc:";
        public string TagId { get; set; } = "   @tc:";
        public ulong ProjectId { get; set; }
        public ulong SuiteId { get; set; }
        public ulong TemplateId { get; set; }
        public string TestRailBaseUrl { get; set; } 
        public string TestRailUserName { get; set; }
        public string TestRailPassword { get; set; }
        public string BaseDirectory { get; set; } = Directory.GetCurrentDirectory();
    }
}