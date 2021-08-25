using System;
using System.IO;

namespace GherkinSyncTool.Configuration
{
    public class Config
    {
        public string BaseDirectory
        {
            get => _directory;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(
                        "Parameter BaseDirectory must not be empty! Please check your settings");
                var info = new DirectoryInfo(value);
                if (!info.Exists)
                    throw new DirectoryNotFoundException($"Directory {value} not found, please, check the path");
                _directory = value;
            }
        }

        public string TagIdPrefix { get; set; } = "@tc:";


        public FormattingSettings FormattingSettings { get; set; }
        public TestRailSettings TestRailSettings { get; set; }

        private string _directory;
    }

    public class TestRailSettings
    {
        private int? _testRailPauseBetweenRetriesSeconds;
        private int? _testRailRetriesCount;

        public ulong TestRailProjectId { get; set; }
        public ulong TestRailSuiteId { get; set; }
        public ulong TestRailTemplateId { get; set; }
        public string TestRailBaseUrl { get; set; }
        public string TestRailUserName { get; set; }
        public string TestRailPassword { get; set; }

        public int? TestRailRetriesCount
        {
            get => _testRailRetriesCount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Attempts count must be a positive number");
                _testRailRetriesCount = value ?? 3;
            }
        }

        public int? TestRailPauseBetweenRetriesSeconds
        {
            get => _testRailPauseBetweenRetriesSeconds;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Pause between requests must be a positive number");
                _testRailPauseBetweenRetriesSeconds = value ?? 5;
            }
        }
    }

    public class FormattingSettings
    {
        public string TagIndentation { get; set; } = "  ";
    }
}