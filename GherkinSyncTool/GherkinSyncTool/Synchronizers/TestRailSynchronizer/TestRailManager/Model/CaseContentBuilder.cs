using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Gherkin.Ast;
using GherkinSyncTool.Interfaces;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model
{
    public class CaseContentBuilder
    {
        public CreateCaseRequest BuildCreateCaseRequest(Scenario scenario, ulong sectionId, IFeatureFile featureFile,
            ulong templateId)
        {
            var background = featureFile.Document.Feature.Children.OfType<Background>().FirstOrDefault();
            var backgroundSteps = background?.Steps.Select(step => step.Keyword + step.Text).ToList();
            var scenarioSteps = scenario.Steps.Select(step => step.Keyword + step.Text).ToList();

            var steps = background is not null ? backgroundSteps.Concat(scenarioSteps).ToList() : scenarioSteps;

            var customStepsSeparated = steps.Select(step => new CustomStepsSeparated {Content = step}).ToList();
            //TODO: fix TestRail client to be able to send template_id parameter with the addCase request
            var customSteps = string.Join(Environment.NewLine, steps);

            var createCaseRequest = new CreateCaseRequest
            {
                Title = scenario.Name,
                SectionId = sectionId,
                CustomFields = new CaseCustomFields
                {
                    CustomPreconditions = ProducePreconditions(scenario, featureFile),
                    CustomStepsSeparated = customStepsSeparated,
                    CustomSteps = customSteps
                },
                TemplateId = templateId,
            };
            return createCaseRequest;
        }

        private static string ProducePreconditions(Scenario scenario, IFeatureFile featureFile)
        {
            var preconditions = new StringBuilder();
            preconditions.Append($"### Feature: {featureFile.Document.Feature.Name}");
            preconditions.AppendLine(featureFile.Document.Feature.Description);
            preconditions.AppendLine($"### Scenario: {scenario.Name}");
            preconditions.AppendLine(scenario.Description);
            return preconditions.ToString();
        }

        public UpdateCaseRequest BuildUpdateCaseRequest(Tag tagId, Scenario scenario, ulong sectionId,
            IFeatureFile featureFile,
            ulong templateId)
        {
            var id = UInt64.Parse(Regex.Match(tagId.Name, @"\d+").Value);
            
            var updateCaseRequest = new UpdateCaseRequest
            {
                CaseId = id,
                Title = scenario.Name
            };
            return updateCaseRequest;
        }
    }
}