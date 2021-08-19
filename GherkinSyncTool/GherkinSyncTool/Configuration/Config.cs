﻿using System;
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
        
        private int? _testRailRequestAttemptsCount;

        public int? TestRailRequestAttemptsCount
        {
            get => _testRailRequestAttemptsCount;
            set
            {
                if (value < 0) 
                    throw new ArgumentException("Attempts count must be a positive number");
                _testRailRequestAttemptsCount = value;
            }
        }

        private int? _testRailPauseBetweenAttemptsSeconds;

        public int? TestRailPauseBetweenAttemptsSeconds
        {
            get =>_testRailPauseBetweenAttemptsSeconds; 
            set
            {
                if (value < 0) 
                    throw new ArgumentException("Pause between requests must be a positive number");
                _testRailPauseBetweenAttemptsSeconds = value;
            }
        }
        public int? TestRailMaxRequestsPerMinute { get; set; }
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