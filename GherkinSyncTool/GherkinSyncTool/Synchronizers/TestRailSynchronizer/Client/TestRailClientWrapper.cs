using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using GherkinSyncTool.Exceptions;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Model;
using Newtonsoft.Json.Linq;
using NLog;
using TestRail;
using TestRail.Types;
using TestRail.Utils;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.Client
{
    public class TestRailClientWrapper
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClient _testRailClient;

        public TestRailClientWrapper(TestRailClient testRailClient)
        {
            _testRailClient = testRailClient;
        }

        public Case AddCase(CaseRequest caseRequest)
        {
            var addCaseResponse =
                _testRailClient.AddCase(caseRequest.SectionId, caseRequest.Title, null, null, null, null,null,
                    caseRequest.JObjectCustomFields, caseRequest.TemplateId);

            ValidateRequestResult(addCaseResponse);

            Log.Info($"Created: [{addCaseResponse.Payload.Id}] {addCaseResponse.Payload.Title}");
            return addCaseResponse.Payload;
        }

        public void UpdateCase(ulong caseId, CaseRequest caseRequest)
        {
            var testRailCase = GetCase(caseId);

            if (!IsTestCaseContentEqual(caseRequest, testRailCase))
            {
                var updateCaseResult = _testRailClient.UpdateCase(caseId, caseRequest.Title, null, null, null, null, null, 
                    caseRequest.JObjectCustomFields, caseRequest.TemplateId);

                ValidateRequestResult(updateCaseResult);

                Log.Info($"Updated: [{caseId}] {caseRequest.Title}");
            }
            else
            {
                Log.Info($"Up-to-date: [{caseId}] {caseRequest.Title}");
            }
        }

        public Case GetCase(ulong id)
        {
            var testRailCase = _testRailClient.GetCase(id);

            ValidateRequestResult(testRailCase);

            return testRailCase.Payload;
        }

        public ulong? CreateSection(CreateSectionRequest request)
        {
            var response = _testRailClient.AddSection(
                request.ProjectId,
                request.SuiteId,
                request.Name, 
                request.ParentId, 
                request.Description);
            
            ValidateRequestResult(response);
            Log.Info($"Section created: [{response.Payload.Id}] {response.Payload.Name}");

            return response.Payload.Id;
        }

        public IEnumerable<Section> GetSections(ulong projectId)
        {
            var result = _testRailClient.GetSections(projectId);
            ValidateRequestResult(result);
            return result.Payload;
            
        }

        public IEnumerable<Case> GetCases(ulong projectId, ulong suiteId)
        {
            var result = _testRailClient.GetCases(projectId, suiteId);
            ValidateRequestResult(result);
            return result.Payload;
        }

        private static bool IsTestCaseContentEqual(CaseRequest caseRequest, Case testRailCase)
        {
            if(!testRailCase.Title.Equals(caseRequest.Title)) return false;
            if(!testRailCase.TemplateId.Equals(caseRequest.TemplateId)) return false;

            var testRailCaseCustomFields = testRailCase.JsonFromResponse.ToObject<CaseCustomFields>();
            if (!JToken.DeepEquals(caseRequest.JObjectCustomFields, JObject.FromObject(testRailCaseCustomFields))) return false;
            
            return true;
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