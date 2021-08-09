using System;
using System.Net;
using System.Reflection;
using GherkinSyncTool.Exceptions;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager.Model;
using Newtonsoft.Json.Linq;
using NLog;
using TestRail;
using TestRail.Types;
using TestRail.Utils;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.TestRailManager
{
    public class TestRailClientWrapper
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClient _testRailClient;

        public TestRailClientWrapper(TestRailClient testRailClient)
        {
            _testRailClient = testRailClient;
        }

        public Case AddCase(CreateCaseRequest createCaseRequest)
        {
            //TODO: required filed logic
            JObject customParam = new JObject(new JProperty("custom_automation_coverage", 1));

            //TODO: handle customParam
            var addCaseResponse =
                _testRailClient.AddCase(createCaseRequest.SectionId, createCaseRequest.Title, createCaseRequest.TypeId,
                    createCaseRequest.PriorityId, createCaseRequest.Estimate, createCaseRequest.MilestoneId,
                    createCaseRequest.Refs, customParam, createCaseRequest.TemplateId);

            ValidateRequestResult(addCaseResponse);

            Log.Info($"Created: [{addCaseResponse.Payload.Id}] {addCaseResponse.Payload.Title}");
            return addCaseResponse.Payload;
        }

        public void UpdateCase(UpdateCaseRequest createCaseRequest)
        {
            var testRailCase = GetCase(createCaseRequest.CaseId);

            //TODO: handle with test case content (not only title) 
            if (!testRailCase.Title.Equals(createCaseRequest.Title))
            {
                var updateCaseResult = _testRailClient.UpdateCase(createCaseRequest.CaseId, createCaseRequest.Title);

                ValidateRequestResult(updateCaseResult);

                Log.Info($"Updated: [{createCaseRequest.CaseId}] {createCaseRequest.Title}");
            }
            else
            {
                Log.Info($"Up-to-date: [{createCaseRequest.CaseId}] {createCaseRequest.Title}");
            }
        }

        public Case GetCase(ulong id)
        {
            var testRailCase = _testRailClient.GetCase(id);

            ValidateRequestResult(testRailCase);

            return testRailCase.Payload;
        }

        private static void ValidateRequestResult<T>(RequestResult<T> requestResult)
        {
            if (requestResult.StatusCode != HttpStatusCode.OK)
            {
                throw new TestRailException(
                    $"There is an issue with requesting TestRail: {requestResult.StatusCode.ToString()} {Environment.NewLine}{requestResult.RawJson}",
                    requestResult.ThrownException);
            }
        }
    }
}