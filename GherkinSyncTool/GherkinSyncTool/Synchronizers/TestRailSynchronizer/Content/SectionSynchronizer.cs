using System;
using System.Collections.Generic;
using System.Linq;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Client;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Model;
using TestRail.Types;
using Config = GherkinSyncTool.Configuration.Config;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.Content
{
    public class SectionSynchronizer
    {
        private readonly TestRailClientWrapper _testRailClientWrapper;
        private readonly Config _config = ConfigurationManager.GetConfiguration();
        
        public SectionSynchronizer(TestRailClientWrapper testRailClientWrapper)
        {
            _testRailClientWrapper = testRailClientWrapper;
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
            
            var testRailSections = _testRailClientWrapper.GetSections(projectId)
                .Select(s=>new TestRailSection(s))
                .ToDictionary(k => k.Id);
            
            var testRailCases = _testRailClientWrapper
                .GetCases(projectId, suiteId.Value)
                .GroupBy(k=>k.SectionId)
                .ToDictionary(k=>k.Key, k=>k.ToArray());

            var result = new List<TestRailSection>();
            foreach (var section in testRailSections.Values)
            {
                if (testRailCases.TryGetValue(section.Id, out Case[] value))
                    testRailSections[section.Id].FeatureFiles.AddRange(value);
                if (section.ParentId != null)
                    testRailSections[section.ParentId].ChildSections.Add(section);
                else result.Add(section);
            }
            return result;
        }

        /// <summary>
        /// Gets or creates TestRail section Id for selected .feature file
        /// </summary>
        /// <param name="path">Path to .feature file</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ulong GetOrCreateSectionId(string path)
        {
            var suiteId = _config.TestRailSuiteId;
            var projectId = _config.TestRailProjectId;
            
            var targetSections = GetSectionsTree(projectId, suiteId);
            //Path includes name of the feature file - hence SkipLast(1)
            var sourceSections = new Queue<string>(path.Split('\\').SkipLast(1));
            return GetOrCreateSectionIdRecursively(targetSections, sourceSections, suiteId, projectId);
        }

        /// <summary>
        /// Compares section structures in TestRail and local storage
        /// and returns or creates (if not existed) section Id for the selected .feature file 
        /// </summary>
        /// <param name="targetSections">Collection that represents section structure in TestRail</param>
        /// <param name="sourceSections">Queue of local folder names from test files root to target file folder</param>
        /// <param name="suiteId">TestRail suite Id</param>
        /// <param name="projectId">TestRail project Id</param>
        /// <param name="sectionId">TestRail section Id, null for the tests root</param>
        /// <returns>Section Id for the selected .feature file</returns>
        private ulong GetOrCreateSectionIdRecursively(IEnumerable<TestRailSection> targetSections, Queue<string> sourceSections, 
            ulong suiteId, ulong projectId, ulong? sectionId = null)
        {
            var targetSectionsChecked = false;
            while (sourceSections.Count != 0)
            {
                var folderName = sourceSections.Dequeue();
                if(!targetSectionsChecked)
                {
                    foreach (var section in targetSections)
                    {
                        if (section.Name != folderName) continue;
                        return GetOrCreateSectionIdRecursively(section.ChildSections, sourceSections, suiteId, projectId, section.Id);
                    }
                    targetSectionsChecked = true;
                }
                sectionId = _testRailClientWrapper.CreateSection(new CreateSectionRequest
                {
                    SuiteId = suiteId,
                    ProjectId = projectId,
                    Name = folderName,
                    ParentId = sectionId
                });
            }
            return sectionId.Value;
        }
    }
}