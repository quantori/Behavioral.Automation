using System.IO;

namespace GherkinSyncTool.Configuration
{
    public class Config
    {
        public string TagIdPattern { get; set; } = "tc:";
        public string TagId { get; set; } = "   @tc:";
        public ulong TestRailProjectId { get; set; }
        public ulong TestRailSuiteId { get; set; }
        public ulong TestRailTemplateId { get; set; }
        public string TestRailBaseUrl { get; set; } 
        public string TestRailUserName { get; set; }
        public string TestRailPassword { get; set; }
        private string _directory;
        public string BaseDirectory
        {
            get => _directory ?? Directory.GetCurrentDirectory();
            set
            {
                _directory = Path.GetRelativePath(Directory.GetCurrentDirectory(), value);
            }
        }
    }
}