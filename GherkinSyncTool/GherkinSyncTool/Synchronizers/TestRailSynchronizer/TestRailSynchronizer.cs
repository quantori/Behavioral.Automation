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

        public TestRailSynchronizer(TestRailClientWrapper testRailClientWrapper, CaseContentBuilder caseContentBuilder)
        {
            _testRailClientWrapper = testRailClientWrapper;
            _caseContentBuilder = caseContentBuilder;
        }

        public void Sync(List<IFeatureFile> featureFiles)
        {
            Log.Info($"# Start synchronization with TestRail");
            var config = ConfigurationManager.GetConfiguration();
            var stopwatch = Stopwatch.StartNew();
            foreach (var featureFile in featureFiles)
            {
                int insertedTagIds = 0;
                foreach (var scenario in featureFile.Document.Feature.Children.OfType<Scenario>())
                {
                    var tagId = scenario.Tags.FirstOrDefault(tag => Regex.Match(tag.Name, config.TagIdPattern, RegexOptions.IgnoreCase).Success);

                    var caseRequest = _caseContentBuilder.BuildCaseRequest(scenario, featureFile);
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
                        var id = UInt64.Parse(Regex.Match(tagId.Name, @"\d+").Value);
 
                        _testRailClientWrapper.UpdateCase(id, caseRequest);
                    }
                }
            }
            Log.Info(@$"Synchronization with TestRail finished in: {stopwatch.Elapsed:mm\:ss\.fff}");
        }

        private static void InsertLineToTheFile(string path, int lineNumber, string text)
        {
            var featureFIleLines = File.ReadAllLines(path).ToList();
            featureFIleLines.Insert(lineNumber, text);
            File.WriteAllLines(path, featureFIleLines);
        }
    }
}