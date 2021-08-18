using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using GherkinSyncTool.Configuration;
using GherkinSyncTool.Exceptions;
using GherkinSyncTool.Synchronizers.TestRailSynchronizer.Model;
using Newtonsoft.Json.Linq;
using NLog;
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

        /// <summary>
        /// Pauses sending of requests when requests per minutes limit is reached 
        /// </summary>
        /// <param name="elapsedMilliSeconds">Milliseconds elapsed from start</param>
        public void RequestsLimitCheck(double elapsedMilliSeconds)
        {
            //To ensure that limiter works even when one or more minute has passed
            elapsedMilliSeconds %= 60_000;
            if (RequestsCount + 1 >= _config.TestRailMaxRequestsPerMinute &&
                elapsedMilliSeconds <= 59_000)
            {
                //additional 3000 milliseconds of sleep - just in case
                var sleepTime = (int) Math.Round(63_000 - elapsedMilliSeconds);
                Log.Debug($"Limit of {_config.TestRailMaxRequestsPerMinute} requests per minute is reached. Waiting for {sleepTime} seconds to continue...");
                Thread.Sleep(sleepTime);
                RequestsCount = 0;
                Log.Debug($"Waiting completed. Requests count set to 0");
            }
            else if (RequestsCount>=180) 
                RequestsCount = 0;
        }
    }
}