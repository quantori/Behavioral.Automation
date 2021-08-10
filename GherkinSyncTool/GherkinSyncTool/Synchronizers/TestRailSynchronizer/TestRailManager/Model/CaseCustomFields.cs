using System.Collections.Generic;
using Newtonsoft.Json;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model
{
    //TODO: improve to use own test case template
    public class CaseCustomFields
    {
        [JsonProperty("custom_preconds")] 
        public string CustomPreconditions { get; init; }
        
        [JsonProperty("custom_automation_coverage")]
        public int CustomAutomationCoverage { get; init; } = 1;

        [JsonProperty("custom_steps_separated")]
        public List<CustomStepsSeparated> CustomStepsSeparated { get; init; }
        
        [JsonProperty("custom_steps")] 
        public string CustomSteps { get; init; }
    }
    
    public class CustomStepsSeparated
    {
        [JsonProperty("content")] 
        public string Content { get; init; }
    }
}