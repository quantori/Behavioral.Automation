using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
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
            _attemptsCount = _config.TestRailRequestAttemptsCount ?? 3;
            _sleepDuration = _config.TestRailPauseBetweenAttemptsSeconds ?? 5;
        }

        public Case AddCase(CreateCaseRequest createCaseRequest)
        {
            var policy = CreatePolicy<Case>();
            
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
            var policy = CreatePolicy<Case>();
            
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
            var policy = CreatePolicy<Case>();
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
            var policy = CreatePolicy<Section>();
            
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
            var policy = CreatePolicy<IList<Section>>();
            var result = policy.Execute(()=>_testRailClient.GetSections(projectId));
            ValidateRequestResult(result);
            return result.Payload;
        }

        public IEnumerable<Case> GetCases(ulong projectId, ulong suiteId)
        {
            var policy = CreatePolicy<IList<Case>>();
            var result = policy.Execute(()=> _testRailClient.GetCases(projectId, suiteId));
            ValidateRequestResult(result);
            return result.Payload;
        }

        /// <summary>
        /// Pauses sending of requests when requests per minutes limit is reached 
        /// </summary>
        /// <param name="elapsedMilliSeconds">Milliseconds elapsed from start</param>
        public void RequestsLimitCheck(double elapsedMilliSeconds)
        {
            var maxRequests = _config.TestRailMaxRequestsPerMinute;
            //To ensure that limiter works even when one or more minute has passed
            elapsedMilliSeconds %= 60_000;
            if (RequestsCount + 1 >= maxRequests &&
                elapsedMilliSeconds <= 59_000)
            {
                //additional 3000 milliseconds of sleep - just in case
                var sleepTime = (int) Math.Round(63_000 - elapsedMilliSeconds);
                Log.Debug($"Limit of {_config.TestRailMaxRequestsPerMinute} requests per minute is reached. Waiting for {sleepTime} seconds to continue...");
                Thread.Sleep(sleepTime);
                RequestsCount = 0;
                Log.Debug($"Waiting completed. Requests count set to 0");
            }
            else if (RequestsCount >=_config.TestRailMaxRequestsPerMinute) 
                RequestsCount = 0;
        }

        /// <summary>
        /// RetryPolicy for request result of given type 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private RetryPolicy<RequestResult<T>> CreatePolicy<T>()
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