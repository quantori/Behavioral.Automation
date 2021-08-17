using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GherkinSyncTool.Configuration;
using System.Reflection;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Client;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Model;
using NLog;
using TestRail.Types;
using Config = GherkinSyncTool.Configuration.Config;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.Content
{
    public class SectionSynchronizer
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClientWrapper _testRailClientWrapper;
        private readonly Config _config = ConfigurationManager.GetConfiguration();
        private List<TestRailSection> _testRailSections;

        public SectionSynchronizer(TestRailClientWrapper testRailClientWrapper)
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

            var testRailSectionsDictionary = _testRailClientWrapper
                .GetSections(projectId)
                .Select(s => new TestRailSection(s))
                .ToDictionary(k => k.Id);

            var testRailCases = _testRailClientWrapper
                .GetCases(projectId, suiteId.Value)
                .GroupBy(k => k.SectionId)
                .ToDictionary(k => k.Key, k => k.ToArray());

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
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public ulong GetOrCreateSectionId(string path)
        {
            var suiteId = _config.TestRailSuiteId;
            var projectId = _config.TestRailProjectId;
            //Path includes name of the feature file - hence SkipLast(1)
            Log.Info($"Input file: {path}");
            var sourceSections = new Queue<string>(path.Split(Path.DirectorySeparatorChar).SkipLast(1));
            return GetOrCreateSectionIdRecursively(_testRailSections, sourceSections, suiteId, projectId);
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
        private ulong GetOrCreateSectionIdRecursively(List<TestRailSection> targetSections,
            Queue<string> sourceSections,
            ulong suiteId, ulong projectId, ulong? sectionId = null)
        {
            var targetSectionsChecked = false;
            if (!sourceSections.Any() && sectionId is null)
                throw new InvalidOperationException(
                    "Attempt to create test case without setting the correct folder. Please check configuration file");
            while (sourceSections.Count != 0)
            {
                var folderName = sourceSections.Dequeue();
                if (!targetSectionsChecked)
                {
                    foreach (var section in targetSections)
                    {
                        if (section.Name != folderName) continue;
                        return GetOrCreateSectionIdRecursively(section.ChildSections, sourceSections, suiteId, projectId, section.Id);
                    }
                    targetSectionsChecked = true;
                }
                var parentId = sectionId;
                sectionId = _testRailClientWrapper.CreateSection(new CreateSectionRequest(projectId, sectionId, suiteId, folderName, null));
                targetSections = CreateChildSection(sectionId, suiteId, parentId, folderName, targetSections);
            }
            return sectionId.Value;
        }

        /// <summary>
        /// Creates new section (only in local structure) without need of sending request to TestRail API
        /// </summary>
        /// <param name="suiteId">TestRail suite Id</param>
        /// <param name="sectionId">TestRail section Id, null for the tests root</param>
        /// <param name="parentId">id of parent Section</param>
        /// <param name="folderName">name of the folder that represents </param>
        /// <param name="targetSections">collection of sections for the new section to add</param>
        /// <returns></returns>
        private List<TestRailSection> CreateChildSection(ulong? sectionId, ulong suiteId, ulong? parentId, 
            string folderName, List<TestRailSection> targetSections)
        {
            var newSection =
                new TestRailSection
                {
                    Id = sectionId,
                    SuiteId = suiteId,
                    ParentId = parentId,
                    Name = folderName
                };
            targetSections.Add(newSection);
            return newSection.ChildSections;
        }
    }
}