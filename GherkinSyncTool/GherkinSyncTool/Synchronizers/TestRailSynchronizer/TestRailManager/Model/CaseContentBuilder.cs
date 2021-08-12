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
                    CustomPreconditions = ConvertToStringPreconditions(scenario, featureFile),
                    CustomStepsSeparated = ConvertToCustomStepsSeparated(steps),
                    CustomSteps = ConvertToStringWithSteps(steps)
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

        private List<string> ExtractSteps(List<Step> steps)
        {
            List<string> resultSteps = new List<string>();
            foreach (var step in steps)
            {
                var fullStep = step.Keyword + step.Text;
                
                if (step.Argument is DocString docString)
                {
                    fullStep += Environment.NewLine + docString.Content;
                }
                
                if (step.Argument is DataTable table)
                {
                    fullStep += Environment.NewLine + ConvertToStringTable(table.Rows.ToList());
                }

                resultSteps.Add(fullStep);
            }

            return resultSteps;
        }

        private List<CustomStepsSeparated> ConvertToCustomStepsSeparated(List<string> steps)
        {
            return steps.Select(step => new CustomStepsSeparated {Content = step}).ToList();
        }

        private string ConvertToStringWithSteps(List<string> steps)
        {
            return string.Join(Environment.NewLine, steps.Select(s => "- " + s));
        }

        private string ConvertToStringPreconditions(Scenario scenario, IFeatureFile featureFile)
        {
            var preconditions = new StringBuilder();
            preconditions.Append($"## {featureFile.Document.Feature.Keyword}: {featureFile.Document.Feature.Name}");
            preconditions.AppendLine(featureFile.Document.Feature.Description);
            preconditions.AppendLine($"## {scenario.Keyword}: {scenario.Name}");
            preconditions.AppendLine(scenario.Description);
            
            var examples = scenario.Examples;
            if (examples != null && examples.Any())
            {
                foreach (var example in examples)
                {
                    preconditions.AppendLine($"## {example.Name}");

                    var tableRows = new List<TableRow> {example.TableHeader};
                    tableRows.AddRange(example.TableBody);
                    preconditions.AppendLine(ConvertToStringTable(tableRows));
                }
            }
            return preconditions.ToString();
        }

        private string ConvertToStringTable(List<TableRow> tableRows)
        {
            var table = new StringBuilder();
            table.Append("||");
            
            //Header
            foreach (var cell in tableRows.First().Cells)
            {
                table.Append($"|:{cell.Value}");
            }
            table.AppendLine();
            
            //Table body
            for (int i = 1; i < tableRows.Count; i++)
            {
                table.Append("|");
                
                var row = tableRows[i];
                foreach (var cell in row.Cells)
                {
                    table.Append($"|{cell.Value}");
                }

                table.AppendLine();
            }

            return table.ToString();
        }
    }
}