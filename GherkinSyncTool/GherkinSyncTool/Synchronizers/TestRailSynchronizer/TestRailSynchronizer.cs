using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Gherkin.Ast;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Interfaces;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Client;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Content;
using NLog;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer
{
    public class TestRailSynchronizer : ISynchronizer
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClientWrapper _testRailClientWrapper;
        private readonly CaseContentBuilder _caseContentBuilder;
        private readonly SectionSynchronizer _sectionSynchronizer;

        public TestRailSynchronizer(TestRailClientWrapper testRailClientWrapper, CaseContentBuilder caseContentBuilder,
            SectionSynchronizer sectionSynchronizer)
        {
            _testRailClientWrapper = testRailClientWrapper;
            _caseContentBuilder = caseContentBuilder;
            _sectionSynchronizer = sectionSynchronizer;
        }

        public void Sync(List<IFeatureFile> featureFiles)
        {
            Log.Info($"# Start synchronization with TestRail");
            var config = ConfigurationManager.GetConfiguration();
            var stopwatch = Stopwatch.StartNew();
            var casesToMove = new Dictionary<ulong, List<ulong>>(); 
            foreach (var featureFile in featureFiles)
            {
                var insertedTagIds = 0;
                var sectionId = _sectionSynchronizer.GetOrCreateSectionIdFromPath(featureFile.RelativePath);
                foreach (var scenario in featureFile.Document.Feature.Children.OfType<Scenario>())
                {
                    var tagId = scenario.Tags.FirstOrDefault(tag => Regex.Match(tag.Name, config.TagIdPattern, RegexOptions.IgnoreCase).Success);

                    var caseRequest = _caseContentBuilder.BuildCaseRequest(scenario, featureFile, sectionId);
                    //Feature file that first time sync with TestRail, no tag id present.  
                    if (tagId is null)
                    {
                        var addCaseResponse = _testRailClientWrapper.AddCase(caseRequest);
                        
                        InsertLineToTheFile(featureFile.AbsolutePath, scenario.Location.Line - 1 + insertedTagIds, config.TagId + addCaseResponse.Id);
                        insertedTagIds++;
                    }
                    //Update scenarios that have tag id
                    if (tagId is not null)
                    {
                        var caseId = UInt64.Parse(Regex.Match(tagId.Name, @"\d+").Value);
                        var updatedCase = _testRailClientWrapper.UpdateCase(caseId, caseRequest);
                        AddCasesToMove(updatedCase?.SectionId, caseRequest.SectionId, caseId, casesToMove);
                    }
                }
            }
            //Moving cases to new sections
            foreach (var (key, value) in casesToMove)
            {   
                _testRailClientWrapper.MoveCases(key, value);
            }
            Log.Info(@$"Synchronization with TestRail finished in: {stopwatch.Elapsed:mm\:ss\.fff}");
        }

        /// <summary>
        /// Adds cases to move to new section, if changed
        /// </summary>
        /// <param name="oldSectionId">id of old section</param>
        /// <param name="currentSectionId">id of current section</param>
        /// <param name="caseId">case id</param>
        /// <param name="casesToMove">IDictionary where key is section id and the value is case ids collection</param>
        private void AddCasesToMove(ulong? oldSectionId, ulong? currentSectionId, ulong caseId, IDictionary<ulong, List<ulong>> casesToMove)
        {
            if (oldSectionId.HasValue && currentSectionId.HasValue &&
                !oldSectionId.Equals(currentSectionId))
            {
                var key = currentSectionId.Value;
                if (!casesToMove.ContainsKey(key))
                    casesToMove.Add(key, new List<ulong>() { caseId });
                else casesToMove[key].Add(caseId);
            }
        }

        private static void InsertLineToTheFile(string path, int lineNumber, string text)
        {
            var featureFIleLines = File.ReadAllLines(path).ToList();
            featureFIleLines.Insert(lineNumber, text);
            File.WriteAllLines(path, featureFIleLines);
        }
    }
}