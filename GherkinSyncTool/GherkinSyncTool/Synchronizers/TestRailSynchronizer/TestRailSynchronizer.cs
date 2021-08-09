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
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model;
using NLog;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer
{
    public class TestRailSynchronizer : ISynchronizer
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClientWrapper _testRailClientWrapper;

        public TestRailSynchronizer(TestRailClientWrapper testRailClientWrapper)
        {
            _testRailClientWrapper = testRailClientWrapper;
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
                    
                    //Feature file that first time sync with TestRail, no tag id present.  
                    if (tagId is null)
                    {
                        //TODO: section selection logic
                        ulong sectionId = 2197;
                        
                        //TODO: creator for CreateCaseRequest
                        var createCaseRequest = new CreateCaseRequest()
                        {
                            Title = scenario.Name,
                            SectionId = sectionId,
                            
                        };
                        var addCaseResponse = _testRailClientWrapper.AddCase(createCaseRequest);
                        
                        InsertLineToTheFile(featureFile.Path, scenario.Location.Line - 1 + insertedTagIds, Config.TagId + addCaseResponse.Id);
                        insertedTagIds++;
                    }
                    
                    //Update scenarios that have tag id
                    if (tagId is not null)
                    {
                        var id = UInt64.Parse(Regex.Match(tagId.Name, @"\d+").Value);
                        
                        //TODO: creator for CreateCaseRequest
                        var createCaseRequest = new UpdateCaseRequest
                        {
                            CaseId = id,
                            Title = scenario.Name
                        };
                        _testRailClientWrapper.UpdateCase(createCaseRequest);
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