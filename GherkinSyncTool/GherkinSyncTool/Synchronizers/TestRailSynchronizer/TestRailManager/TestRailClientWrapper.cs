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
            var addCaseResponse =
                _testRailClient.AddCase(createCaseRequest.SectionId, createCaseRequest.Title, createCaseRequest.TypeId,
                    createCaseRequest.PriorityId, createCaseRequest.Estimate, createCaseRequest.MilestoneId,
                    createCaseRequest.Refs, JObject.FromObject(createCaseRequest.CustomFields), createCaseRequest.TemplateId);

            ValidateRequestResult(addCaseResponse);

            Log.Info($"Created: [{addCaseResponse.Payload.Id}] {addCaseResponse.Payload.Title}");
            return addCaseResponse.Payload;
        }

        public void UpdateCase(UpdateCaseRequest updateCaseRequest)
        {
            var testRailCase = GetCase(updateCaseRequest.CaseId);

            //TODO: handle with test case content (not only title) 
            if (!testRailCase.Title.Equals(updateCaseRequest.Title))
            {
                var updateCaseResult = _testRailClient.UpdateCase(updateCaseRequest.CaseId, updateCaseRequest.Title);

                ValidateRequestResult(updateCaseResult);

                Log.Info($"Updated: [{updateCaseRequest.CaseId}] {updateCaseRequest.Title}");
            }
            else
            {
                Log.Info($"Up-to-date: [{updateCaseRequest.CaseId}] {updateCaseRequest.Title}");
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