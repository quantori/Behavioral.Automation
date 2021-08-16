using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model;
using NLog;
using TestRail.Types;
using Config = GherkinSyncTool.Configuration.Config;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer
{
    public class TestRailSectionSynchronizer
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClientWrapper _testRailClientWrapper;
        private List<TestRailSection> _testRailSections;
        
        public TestRailSectionSynchronizer(TestRailClientWrapper testRailClientWrapper)
        {
            _testRailClientWrapper = testRailClientWrapper;
            var config = ConfigurationManager.GetConfiguration();
            _testRailSections = GetSectionsTree(config.TestRailProjectId, config.TestRailSuiteId).ToList();
        }
        
        /// <summary>
        /// Builds a tree structure for TestRail sections
        /// </summary>
        /// <param name="projectId">TestRail project Id</param>
        /// <param name="suiteId">TestRail suite Id</param>
        /// <returns></returns>
        private IEnumerable<TestRailSection> GetSectionsTree(ulong projectId, ulong? suiteId)
        {
            if (suiteId is null) 
                throw new ArgumentException($"SuiteId must be specified. Check the TestRail project #{projectId}");
            
            var testRailSectionsDictionary = _testRailClientWrapper.GetSections(projectId)
                .Select(s=>new TestRailSection(s))
                .ToDictionary(k => k.Id);
            
            var testRailCases = _testRailClientWrapper
                .GetCases(projectId, suiteId.Value)
                .GroupBy(k=>k.SectionId)
                .ToDictionary(k=>k.Key, k=>k.ToArray());

            var result = new List<TestRailSection>();
            foreach (var section in testRailSectionsDictionary.Values)
            {
                if (testRailCases.TryGetValue(section.Id, out Case[] value))
                    testRailSectionsDictionary[section.Id].FeatureFiles.AddRange(value);
                if (section.ParentId != null)
                    testRailSectionsDictionary[section.ParentId].ChildSections.Add(section);
                else result.Add(section);
            }
            return result;
        }

        /// <summary>
        /// Gets or creates TestRail section Id for selected .feature file
        /// </summary>
        /// <param name="path">Path to .feature file</param>
        /// <param name="suiteId">TestRail suite Id</param>
        /// <param name="projectId">TestRail project Id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ulong GetOrCreateSectionId(string path, ulong suiteId, ulong projectId)
        {
            //Path includes name of the feature file - hence SkipLast(1)
            var sourceSections = new Queue<string>(path.Split('\\').SkipLast(1));
            return GetOrCreateSectionIdRecursively(_testRailSections, sourceSections, suiteId, projectId);
        }

        /// <summary>
        /// Compares section structures in TestRail and local storage
        /// and returns or creates (if not existed) section Id for the selected .feature file 
        /// </summary>
        /// <param name="sourceSections">Queue of local folder names from test files root to target file folder</param>
        /// <param name="suiteId">TestRail suite Id</param>
        /// <param name="projectId">TestRail project Id</param>
        /// <param name="sectionId">TestRail section Id, null for the tests root</param>
        /// <returns>Section Id for the selected .feature file</returns>
        private ulong GetOrCreateSectionIdRecursively(List<TestRailSection> targetSections, Queue<string> sourceSections, 
            ulong suiteId, ulong projectId, ulong? sectionId = null)
        {
            var targetSectionsChecked = false;
            if (sourceSections.Count == 0 && sectionId is null)
                throw new InvalidOperationException("Attempt to create test case without setting the correct folder. Please check configuration file");
            while (sourceSections.Count != 0)
            {
                var folderName = sourceSections.Dequeue();
                if(!targetSectionsChecked)
                {
                    foreach (var section in targetSections)
                    {
                        if (section.Name != folderName) continue;
                        Log.Info($"Opening {folderName} section");
                        return GetOrCreateSectionIdRecursively(section.ChildSections, sourceSections, suiteId, projectId, section.Id);
                    }
                    targetSectionsChecked = true;
                }
                var parentId = sectionId;
                sectionId = _testRailClientWrapper.CreateSection(new CreateSectionRequest
                {
                    SuiteId = suiteId,
                    ProjectId = projectId,
                    Name = folderName,
                    ParentId = sectionId
                });
                var newSection = 
                new TestRailSection
                {
                    Id = sectionId, 
                    SuiteId = suiteId, 
                    ParentId = parentId, 
                    Name = folderName
                };
                targetSections.Add(newSection);
                targetSections = newSection.ChildSections;
            }
            return sectionId.Value;
        }
    }
}