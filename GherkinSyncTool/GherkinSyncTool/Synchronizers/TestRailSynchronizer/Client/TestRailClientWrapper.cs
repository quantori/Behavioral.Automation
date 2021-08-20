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

        private int? _requestsCount;
        private readonly Config _config;
        private readonly int _attemptsCount;
        private readonly int _sleepDuration;

        public int? RequestsCount
        {
            get => _requestsCount;
            set
            {
                if (value < 0) 
                    throw new ArgumentException("Number of requests per minute must be positive");
                _requestsCount = value;
            }
        }

        public TestRailClientWrapper(TestRailClient testRailClient)
        {
            _testRailClient = testRailClient;
            _config = ConfigurationManager.GetConfiguration();
            RequestsCount ??= 0;
            _attemptsCount = _config.TestRailRetriesCount ?? 3;
            _sleepDuration = _config.TestRailPauseBetweenRetriesSeconds ?? 5;
        }

        public Case AddCase(CreateCaseRequest createCaseRequest)
        {
            var policy = CreateResultHandlerPolicy<Case>();
            
            var addCaseResponse = policy.Execute(()=>
                _testRailClient.AddCase(createCaseRequest.SectionId, createCaseRequest.Title, createCaseRequest.TypeId,
                    createCaseRequest.PriorityId, createCaseRequest.Estimate, createCaseRequest.MilestoneId,
                    createCaseRequest.Refs, JObject.FromObject(createCaseRequest.CustomFields), createCaseRequest.TemplateId));

            ValidateRequestResult(addCaseResponse);

            Log.Info($"Created: [{addCaseResponse.Payload.Id}] {addCaseResponse.Payload.Title}");
            return addCaseResponse.Payload;
        }

        public void UpdateCase(UpdateCaseRequest updateCaseRequest)
        {
            var policy = CreateResultHandlerPolicy<Case>();
            
            var testRailCase = GetCase(updateCaseRequest.CaseId);
            
            //TODO: handle with test case content (not only title) 
            if (!testRailCase.Title.Equals(updateCaseRequest.Title))
            {
                var updateCaseResult = policy.Execute(()=>
                    _testRailClient.UpdateCase(updateCaseRequest.CaseId, updateCaseRequest.Title));
                
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
                    $"There is an issue with requesting TestRail: {requestResult.StatusCode.ToString()} {Environment.NewLine}{requestResult.RawJson}",
                    requestResult.ThrownException);
            }

            Log.Debug($"Requests sent: {++RequestsCount}");
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
    }
}