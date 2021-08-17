using System;
using System.IO;
using System.Reflection;
using NLog;

namespace GherkinSyncTool.Configuration
{
    public class Config
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
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
            get => _directory;
            set
            {
                if (string.IsNullOrEmpty(value)) 
                    throw new ArgumentException("Parameter BaseDirectory must not be empty! Please check your settings");
                var info = new DirectoryInfo(value);
                if (!info.Exists) 
                    throw new DirectoryNotFoundException($"Directory {value} not found, please, check the path");
                _directory = value;
            }
        }
    }
}