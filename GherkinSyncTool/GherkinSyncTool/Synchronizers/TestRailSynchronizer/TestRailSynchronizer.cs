using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
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
        private readonly Config _config;

        public TestRailSynchronizer(TestRailClientWrapper testRailClientWrapper, CaseContentBuilder caseContentBuilder)
        {
            _testRailClientWrapper = testRailClientWrapper;
            _caseContentBuilder = caseContentBuilder;
            _config = ConfigurationManager.GetConfiguration();
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
                    if(_config.MaximumRequestsPerMinute is not null)
                        RequestsLimitCheck(stopwatch.Elapsed.Seconds);
                    //Feature file that first time sync with TestRail, no tag id present.  
                    if (tagId is null)
                    {
                        var createCaseRequest = _caseContentBuilder.BuildCreateCaseRequest(scenario, featureFile);
                        
                        var addCaseResponse = _testRailClientWrapper.AddCase(createCaseRequest);
                        
                        InsertLineToTheFile(featureFile.AbsolutePath, scenario.Location.Line - 1 + insertedTagIds, config.TagId + addCaseResponse.Id);
                        insertedTagIds++;
                    }
                    //Update scenarios that have tag id
                    //TODO: update test case body, not only a title
                    if (tagId is not null)
                    {
                        var updateCaseRequest =
                            _caseContentBuilder.BuildUpdateCaseRequest(tagId, scenario, featureFile);
                        _testRailClientWrapper.UpdateCase(updateCaseRequest);
                    }
                }
            }
            Log.Info(@$"Synchronization with TestRail finished in: {stopwatch.Elapsed:mm\:ss\.fff}");
        }

        /// <summary>
        /// Pauses sending of requests when requests per minutes limit is reached 
        /// </summary>
        /// <param name="elapsedSeconds">Seconds elapsed from start</param>
        private void RequestsLimitCheck(int elapsedSeconds)
        {
            if (_testRailClientWrapper.RequestsCount + 1 >= _config.MaximumRequestsPerMinute &&
               elapsedSeconds % 60 <= 59)
            {
                var sleepTime = 60 - elapsedSeconds;
                Log.Info($"Limit of {_config.MaximumRequestsPerMinute} requests per minute is reached. Waiting for {sleepTime} seconds to continue...");
                Thread.Sleep(sleepTime);
                _testRailClientWrapper.RequestsCount = 0;
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