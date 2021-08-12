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

        public IEnumerable<TestRailSection> GetSectionsTree(ulong projectId, ulong? suiteId)
        {
            var testRailSections = _testRailClientWrapper.GetSections(projectId)
                .Select(s=>new TestRailSection(s))
                .ToDictionary(k => k.Id);
            
            if (suiteId is null) 
                throw new ArgumentException($"SuiteId must be specified. Check the TestRail project #{projectId}");
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

        public ulong GetOrCreateSection(string path, ulong suiteId, ulong projectId)
        {
            var sections = GetSectionsTree(projectId, suiteId);
            //Path includes name of the feature file and files catalog - hence SkipLast(1) and Skip(1)
            var sourceSections = new Queue<string>(path.Split('\\').SkipLast(1).Skip(1));
            var id = TraverseSections(sections, sourceSections, suiteId, projectId);

            return id.Value;
        }

        private ulong? TraverseSections(IEnumerable<TestRailSection> sections, Queue<string> sourceSections, 
            ulong suiteId, ulong projectId, ulong? sectionId = null)
        {
            bool targetSectionsChecked = false;
            while (sourceSections.Count != 0)
            {
                var folderName = sourceSections.Dequeue();
                if(!targetSectionsChecked)
                {
                    foreach (var section in sections)
                    {
                        if (section.Name != folderName) continue;
                        sectionId = TraverseSections(section.ChildSections, sourceSections, suiteId, projectId, section.Id);
                        return sectionId;
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