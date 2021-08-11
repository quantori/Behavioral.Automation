using System;
using System.Collections.Generic;
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
            var steps = GetSteps(scenario, featureFile);

            var createCaseRequest = new CreateCaseRequest
            {
                Title = scenario.Name,
                SectionId = sectionId,
                CustomFields = new CaseCustomFields
                {
                    CustomPreconditions = FormatPreconditions(scenario, featureFile),
                    CustomStepsSeparated = FormatCustomStepsSeparated(steps),
                    CustomSteps = FormatCustomSteps(steps)
                },
                //TODO: fix TestRail client to be able to send template_id parameter with the addCase request
                TemplateId = templateId,
            };
            return createCaseRequest;
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

        private List<string> GetSteps(Scenario scenario, IFeatureFile featureFile)
        {
            var scenarioSteps = ExtractSteps(scenario.Steps.ToList());

            var background = featureFile.Document.Feature.Children.OfType<Background>().FirstOrDefault();
            if (background is not null)
            {
                var backgroundSteps = ExtractSteps(background.Steps.ToList());
                return backgroundSteps.Concat(scenarioSteps).ToList();
            }

            return scenarioSteps;
        }

        private static List<string> ExtractSteps(List<Step> steps)
        {
            List<string> resultSteps = new List<string>();
            foreach (var step in steps)
            {
                var fullStep = step.Keyword + step.Text;
                if (step.Argument is DocString argument)
                {
                    fullStep += Environment.NewLine + argument.Content;
                }

                resultSteps.Add(fullStep);
            }

            return resultSteps;
        }

        private List<CustomStepsSeparated> FormatCustomStepsSeparated(List<string> steps)
        {
            return steps.Select(step => new CustomStepsSeparated {Content = step}).ToList();
        }

        private static string FormatCustomSteps(List<string> steps)
        {
            return string.Join(Environment.NewLine, steps.Select(s => "- " + s));
        }

        private static string FormatPreconditions(Scenario scenario, IFeatureFile featureFile)
        {
            var preconditions = new StringBuilder();
            preconditions.Append($"### Feature: {featureFile.Document.Feature.Name}");
            preconditions.AppendLine(featureFile.Document.Feature.Description);
            preconditions.AppendLine($"### Scenario: {scenario.Name}");
            preconditions.AppendLine(scenario.Description);
            return preconditions.ToString();
        }
    }
}