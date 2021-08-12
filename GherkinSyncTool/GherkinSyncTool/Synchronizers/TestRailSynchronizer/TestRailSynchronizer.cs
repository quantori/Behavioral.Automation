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
using GherkinSyncTool.Synchronizers.SectionsSynchronizer;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model;
using NLog;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer
{
    public class TestRailSynchronizer : ISynchronizer
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClientWrapper _testRailClientWrapper;
        private readonly SectionSynchronizer _sectionSynchronizer;

        public TestRailSynchronizer(TestRailClientWrapper testRailClientWrapper, SectionSynchronizer sectionSynchronizer)
        {
            _testRailClientWrapper = testRailClientWrapper;
            _sectionSynchronizer = sectionSynchronizer;
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
                    //section selection logic
                    ulong suiteId = 77;
                    ulong projectId = 2;
                    ulong sectionId = _sectionSynchronizer.GetOrCreateSection(featureFile.Path, suiteId, projectId);
                    
                    //Feature file that first time sync with TestRail, no tag id present.  
                    if (tagId is null)
                    {
                        //TODO: template selection logic
                        ulong templateId = 2;
                        
                        //TODO: refactoring: creator for CreateCaseRequest
                        var scenarioSteps = scenario.Steps.Select(step => step.Keyword + step.Text).ToList();
                        var customStepsSeparated = scenarioSteps.Select(step => new CustomStepsSeparated {Content = step}).ToList();
                        //TODO: fix TestRail client to be able to send template_id parameter with the addCase request
                        var customSteps = String.Join(Environment.NewLine, scenarioSteps); 

                        var createCaseRequest = new CreateCaseRequest()
                        {
                            Title = scenario.Name,
                            SectionId = sectionId,
                            CustomFields = new CaseCustomFields
                            {
                                CustomPreconditions = featureFile.Document.Feature.Description
                                                  + Environment.NewLine
                                                  + scenario.Description,
                                CustomStepsSeparated = customStepsSeparated,
                                CustomSteps = customSteps
                            },
                            TemplateId = templateId,
                        };
                        
                        var addCaseResponse = _testRailClientWrapper.AddCase(createCaseRequest);
                        
                        InsertLineToTheFile(featureFile.Path, scenario.Location.Line - 1 + insertedTagIds, Config.TagId + addCaseResponse.Id);
                        insertedTagIds++;
                    }
                    
                    //Update scenarios that have tag id
                    //TODO: update test case body, not only a title
                    if (tagId is not null)
                    {
                        var id = UInt64.Parse(Regex.Match(tagId.Name, @"\d+").Value);
                        
                        //TODO: refactoring: creator for UpdateCaseRequest
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