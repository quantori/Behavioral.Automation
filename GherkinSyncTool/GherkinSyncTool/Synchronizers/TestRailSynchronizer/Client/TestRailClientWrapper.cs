using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Exceptions;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Model;
using Newtonsoft.Json.Linq;
using NLog;
using Polly;
using Polly.Retry;
using TestRail;
using TestRail.Types;
using TestRail.Utils;
using Config = GherkinSyncTool.Configuration.Config;

namespace GherkinSyncTool.Synchronizers.TestRailSynchronizer.Client
{
    public class TestRailClientWrapper
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType?.Name);
        private readonly TestRailClient _testRailClient;
        private readonly Config _config;
        private readonly int _attemptsCount;
        private readonly int _sleepDuration;

        private int? _requestsCount;

        public TestRailClientWrapper(TestRailClient testRailClient)
        {
            _testRailClient = testRailClient;
            _config = ConfigurationManager.GetConfiguration();
            _requestsCount ??= 0;
            _attemptsCount = _config.TestRailRetriesCount ?? 3;
            _sleepDuration = _config.TestRailPauseBetweenRetriesSeconds ?? 5;
        }

        public Case AddCase(CaseRequest caseRequest)
        {
            var policy = CreateResultHandlerPolicy<Case>();
            
            var addCaseResponse = policy.Execute(()=>
                _testRailClient.AddCase(caseRequest.SectionId, caseRequest.Title, null,
                    null, null, null, null, 
                    caseRequest.JObjectCustomFields, caseRequest.TemplateId));

            ValidateRequestResult(addCaseResponse);

            Log.Info($"Created: [{addCaseResponse.Payload.Id}] {addCaseResponse.Payload.Title}");
            return addCaseResponse.Payload;
        }

        public void UpdateCase(ulong caseId, CaseRequest caseRequest)
        {
            var policy = CreateResultHandlerPolicy<Case>();
            
            var testRailCase = GetCase(caseId);

            if (!IsTestCaseContentEqual(caseRequest, testRailCase))
            {
                var updateCaseResult = policy.Execute(()=>
                    _testRailClient.UpdateCase(caseId, caseRequest.Title, null, null, null, null, null, 
                        caseRequest.JObjectCustomFields, caseRequest.TemplateId));
                
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
            var policy = CreateResultHandlerPolicy<Case>();
            var testRailCase = policy.Execute(()=>
                _testRailClient.GetCase(id));

            ValidateRequestResult(testRailCase);

            return testRailCase.Payload;
        }

        private void ValidateRequestResult<T>(RequestResult<T> requestResult)
        {
            if (requestResult.StatusCode != HttpStatusCode.OK)
            {
                throw new TestRailException(
                    $"There is an issue with requesting TestRail: {requestResult.StatusCode.ToString()} " +
                    $"{Environment.NewLine}{requestResult.RawJson}",
                    requestResult.ThrownException);
            }

            Log.Debug($"Requests sent: {++_requestsCount}");
        }

        public ulong? CreateSection(CreateSectionRequest request)
        {
            var policy = CreateResultHandlerPolicy<Section>();
            
            var response = policy.Execute(()=>
                _testRailClient.AddSection(
                    request.ProjectId,
                    request.SuiteId,
                    request.Name, 
                    request.ParentId, 
                    request.Description));
            
            ValidateRequestResult(response);
            Log.Info($"Section created: [{response.Payload.Id}] {response.Payload.Name}");

            return response.Payload.Id;
        }

        public IEnumerable<Section> GetSections(ulong projectId)
        {
            var policy = CreateResultHandlerPolicy<IList<Section>>();
            var result = policy.Execute(()=>_testRailClient.GetSections(projectId));
            ValidateRequestResult(result);
            return result.Payload;
        }

        public IEnumerable<Case> GetCases(ulong projectId, ulong suiteId)
        {
            var policy = CreateResultHandlerPolicy<IList<Case>>();
            var result = policy.Execute(()=> _testRailClient.GetCases(projectId, suiteId));
            ValidateRequestResult(result);
            return result.Payload;
        }

        /// <summary>
        /// Moves feature files to new section
        /// </summary>
        /// <param name="newSectionId">Id of destination section</param>
        /// <param name="caseIds">array of feature file id's</param>
        public void MoveCases(ulong newSectionId, IEnumerable<ulong> caseIds)
        {
            var policy = CreateResultHandlerPolicy<Result>();
            var result = policy.Execute(()=>
                _testRailClient.MoveCases(newSectionId, caseIds));
            ValidateRequestResult(result);
            Log.Info($"Cases {string.Join(", ", caseIds)} moved to section {newSectionId}");
        }

        /// <summary>
        /// RetryPolicy for request result of given type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private RetryPolicy<RequestResult<T>> CreateResultHandlerPolicy<T>()
        {
            return Policy.HandleResult<RequestResult<T>>(r=>(int)r.StatusCode < 200 && (int)r.StatusCode > 299)
                .WaitAndRetry(_attemptsCount, retryAttempt =>
                {
                    Log.Debug($"Attempt {retryAttempt} of {_attemptsCount}, waiting for {_sleepDuration}");
                    return TimeSpan.FromSeconds(_sleepDuration);
                });
        }

        private static bool IsTestCaseContentEqual(CaseRequest caseRequest, Case testRailCase)
        {
            if(!testRailCase.Title.Equals(caseRequest.Title)) return false;
            if(!testRailCase.TemplateId.Equals(caseRequest.TemplateId)) return false;

            var testRailCaseCustomFields = testRailCase.JsonFromResponse.ToObject<CaseCustomFields>();
            if (!JToken.DeepEquals(caseRequest.JObjectCustomFields, JObject.FromObject(testRailCaseCustomFields))) return false;
            
            return true;
        }
    }
}
