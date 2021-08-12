using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GherkinSyncTool.Models;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model;
using NLog;
using TestRail.Types;

namespace GherkinSyncTool.Synchronizers.SectionsSynchronizer
{
    public class SectionSynchronizer
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClientWrapper _testRailClientWrapper;
        
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
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<TestRailSection> GetSectionsTree(ulong projectId, ulong? suiteId)
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
        /// <param name="suiteId">TestRail suite Id</param>
        /// <param name="projectId">TestRail project Id</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ulong GetOrCreateSectionId(string path, ulong suiteId, ulong projectId)
        {
            var targetSections = GetSectionsTree(projectId, suiteId);
            //Path includes name of the feature file and files catalog - hence SkipLast(1) and Skip(1)
            var sourceSections = new Queue<string>(path.Split('\\').SkipLast(1).Skip(1));
            var sectionId = GetOrCreateSectionIdRecursively(targetSections, sourceSections, suiteId, projectId);
            if (sectionId is null)
                throw new ArgumentNullException(nameof(sectionId), 
                    "Section id cannot be null. You cannot put feature files in root folder");
            return sectionId.Value;
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
        private ulong? GetOrCreateSectionIdRecursively(IEnumerable<TestRailSection> targetSections, Queue<string> sourceSections, 
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
            return sectionId;
        }
    }
}