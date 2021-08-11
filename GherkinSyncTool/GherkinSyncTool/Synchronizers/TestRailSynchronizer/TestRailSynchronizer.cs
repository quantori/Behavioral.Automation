using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Gherkin.Ast;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Interfaces;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model;
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
            Log.Info($"# Start synchronization whith TestRail");
            var stopwatch = Stopwatch.StartNew();
            foreach (var featureFile in featureFiles)
            {
                int insertedTagIds = 0;
                foreach (var scenario in featureFile.Document.Feature.Children.OfType<Scenario>())
                {
                    var tagId = scenario.Tags.FirstOrDefault(tag => Regex.Match(tag.Name, Config.TagIdPattern, RegexOptions.IgnoreCase).Success);
                    //TODO: section selection logic
                    ulong sectionId = 2197;
                    //TODO: template selection logic
                    ulong templateId = 2;
                    
                    //Feature file that first time sync with TestRail, no tag id present.  
                    if (tagId is null)
                    {
                        var createCaseRequest = _caseContentBuilder.BuildCreateCaseRequest(scenario, sectionId, featureFile, templateId);
                        
                        var addCaseResponse = _testRailClientWrapper.AddCase(createCaseRequest);
                        
                        InsertLineToTheFile(featureFile.Path, scenario.Location.Line - 1 + insertedTagIds, Config.TagId + addCaseResponse.Id);
                        insertedTagIds++;
                    }
                    
                    //Update scenarios that have tag id
                    //TODO: update test case body, not only a title
                    if (tagId is not null)
                    {
                        var updateCaseRequest =
                            _caseContentBuilder.BuildUpdateCaseRequest(tagId, scenario, sectionId, featureFile,
                                templateId);
                        _testRailClientWrapper.UpdateCase(updateCaseRequest);
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