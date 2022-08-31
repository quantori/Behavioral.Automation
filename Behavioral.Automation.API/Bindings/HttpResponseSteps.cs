using System.Text.RegularExpressions;
using Behavioral.Automation.API.Context;
using Behavioral.Automation.API.Models;
using Behavioral.Automation.API.Services;
using Behavioral.Automation.Configs.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Polly;
using TechTalk.SpecFlow;

namespace Behavioral.Automation.API.Bindings;

[Binding]
public class HttpResponseSteps
{
    private readonly ApiContext _apiContext;
    private readonly HttpService _httpService;
    private readonly int _retryCount = 10;
    private readonly TimeSpan _retryDelay = TimeSpan.FromSeconds(3);

    public HttpResponseSteps(ApiContext apiContext, HttpService httpService)
    {
        _apiContext = apiContext;
        _httpService = httpService;
    }

    [Then("response attachment filename is \"(.*)\"")]
    public void ThenResponseAttachmentFilenameIs(string filename)
    {
        if (_apiContext.Response is null) throw new NullReferenceException("Http response is empty.");
        var contentDispositionHeader = _apiContext.Response.Content.Headers.ContentDisposition;
        if (contentDispositionHeader == null)
        {
            Assert.Fail("Response header \"ContentDisposition disposition\" is null");
        }
        if (!contentDispositionHeader.ToString().StartsWith("attachment"))
        {
            Assert.Fail("Content disposition is not attachment?");
        }

        if (!contentDispositionHeader.FileName.Equals(filename))
        {
            Assert.Fail($"filename is wrong.\n\nActual result: {contentDispositionHeader.FileName}\nExpected result: {filename}");
        }
    }

    [Given("response attachment is saved as \"(.*)\"")]
    public void GivenResponseAttachmentIsSavedAs(string filePath)
    {
        if (_apiContext.Response is null) throw new NullReferenceException("Http response is empty.");
        var fullPath = Path.GetFullPath(Path.Join(Directory.GetCurrentDirectory(), filePath))
            .NormalizePathAccordingOs();

        var directoryPath = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var responseContentByteArray = _apiContext.Response.Content.ReadAsByteArrayAsync().Result;
        File.WriteAllBytes(fullPath, responseContentByteArray);
    }

    [Then("response json path \"(.*)\" value should match regex \"(.*)\"")]
    public void ThenResponseJsonPathValueShouldMatchRegexp(string jsonPath, string regex)
    {
        var actualJTokens = GetActualJTokensFromResponse(jsonPath);
        if (actualJTokens.Count != 1)
        {
            Assert.Fail($"Error! To check regexp match, json path should return single value.  Number of returned values is {actualJTokens.Count}");
        }
        var stringToCheck = actualJTokens[0].ToString();
        if (!Regex.IsMatch(stringToCheck, regex))
        {
            Assert.Fail($"Response json value '{stringToCheck}' doesn't match regexp {regex}");
        }
    }

    [Then("response json path \"(.*)\" value should (be|become):")]
    [Then("response json path \"(.*)\" value should (be|become) \"(.*)\"")]
    public void CheckResponseJsonPath(string jsonPath, AssertionType assertionType, string expected)
    {
        expected = AddSquareBrackets(expected);

        JToken parsedExpectedJson;
        try
        {
            parsedExpectedJson = JToken.Parse(expected);
        }
        catch (JsonReaderException e)
        {
            throw new ArgumentException($"Error while parsing \"{expected}\". Expected value should be a valid json", e);
        }

        var expectedJTokens = parsedExpectedJson.Select(token => token).ToList();

        var actualJTokens = GetActualJTokensFromResponse(jsonPath);

        if (actualJTokens.Count != expectedJTokens.Count)
        {
            if (assertionType == AssertionType.Become)
            {
                Policy.HandleResult<int>(count => !count.Equals(expectedJTokens.Count))
                    .WaitAndRetry(_retryCount, _ => _retryDelay).Execute(() =>
                    {
                        _httpService.SendContextRequest();
                        actualJTokens = GetActualJTokensFromResponse(jsonPath);
                        return actualJTokens.Count;
                    });
            }

            if (actualJTokens.Count != expectedJTokens.Count)
                FailJTokensAssertion(actualJTokens, expectedJTokens, "Elements count mismatch.");
        }

        if (!IsJTokenListsSimilar(expectedJTokens, actualJTokens))
        {
            if (assertionType == AssertionType.Become)
            {
                Policy.HandleResult<List<JToken>>(_ => !IsJTokenListsSimilar(expectedJTokens, actualJTokens))
                    .WaitAndRetry(_retryCount, _ => _retryDelay).Execute(() =>
                    {
                        _httpService.SendContextRequest();
                        actualJTokens = GetActualJTokensFromResponse(jsonPath);
                        return actualJTokens;
                    });
            }

            if (!IsJTokenListsSimilar(expectedJTokens, actualJTokens))
                FailJTokensAssertion(actualJTokens, expectedJTokens,
                    "The actual result is not equal to the expected result.");
        }
    }

    [Then("response json path \"(.*)\" value should not (be|become) empty")]
    public void CheckResponseJsonPathNotEmpty(string jsonPath, AssertionType assertionType)
    {
        var actualJTokens = GetActualJTokensFromResponse(jsonPath);

        if (actualJTokens.Count == 0)
        {
            if (assertionType == AssertionType.Become)
            {
                Policy.HandleResult<int>(count => count == 0).WaitAndRetry(_retryCount, _ => _retryDelay).Execute(() =>
                {
                    _httpService.SendContextRequest();
                    actualJTokens = GetActualJTokensFromResponse(jsonPath);
                    return actualJTokens.Count;
                });
            }

            if (actualJTokens.Count == 0) Assert.Fail("Expected response json path value is empty");
        }
    }

    [Then("response json path \"(.*)\" count should be \"(\\d*)\"")]
    public void ThenResponseJsonPathValueShouldBecome(string jsonPath, int expectedQuantity)
    {
        var actualQuantity = GetActualJTokensFromResponse(jsonPath).Count;
        Assert.AreEqual(expectedQuantity, actualQuantity);
    }


    [Given("expected response status code is \"(\\d*)\"")]
    public void ChangeResponseStatusCode(int statusCode)
    {
        _apiContext.ExpectedStatusCode = statusCode;
    }

    [Then("response time is less then \"(.*)\" millis")]
    public void ThenResponseTimeIsLessThenMillis(string timeoutString)
    {
        var timeout = Convert.ToInt64(timeoutString);
        Assert.Less(_apiContext.ResponseTimeMillis, timeout,
            $"API response time should be less then {timeout}, but was {_apiContext.ResponseTimeMillis}");
    }

    [Then("response json path \"(.*)\" should be equal to the file \"(.*)\"")]
    public void ThenTheResponseJsonPathShouldBeEqualToFile(string jsonPath, string filePath)
    {
        var fullPath = Path.GetFullPath(Path.Join(Directory.GetCurrentDirectory(), filePath))
            .NormalizePathAccordingOs();
        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException("The file does not exist", fullPath);
        }

        var actualJTokens = GetActualJTokensFromResponse(jsonPath);
        var expectedJTokens = GetExpectedJTokensFromFile(fullPath);
        if (!IsJTokenListsSimilar(expectedJTokens, actualJTokens))
            FailJTokensAssertion(actualJTokens, expectedJTokens,
                "The actual result is not equal to the expected result.");
    }
    
    [Then("response should be \"(.*)\"")]
    [Then("response should be:")]
    public void ThenResponseValueShouldBe(string expected)
    {
        var responseString = _apiContext.Response.Content.ReadAsStringAsync().Result;
        Assert.That(responseString, Is.EqualTo(expected));
    }
    
    private static bool IsJTokenListsSimilar(List<JToken> expectedJTokens, List<JToken> actualJTokens)
    {
        if (expectedJTokens.Count == 0 && actualJTokens.Count == 0) return true;
        bool areEqual = false;
        foreach (var expectedJToken in expectedJTokens)
        {
            foreach (var actualJToken in actualJTokens)
            {
                if (JToken.DeepEquals(expectedJToken, actualJToken))
                {
                    areEqual = true;
                    break;
                }

                areEqual = false;
            }

            if (!areEqual)
            {
                return false;
            }
        }

        return areEqual;
    }

    private List<JToken> GetActualJTokensFromResponse(string jsonPath)
    {
        if (_apiContext.Response is null) throw new NullReferenceException("Http response is empty.");
        var responseString = _apiContext.Response.Content.ReadAsStringAsync().Result;

        JToken responseJToken;
        try
        {
            responseJToken = JToken.Parse(responseString);
        }
        catch (JsonReaderException e)
        {
            throw new ArgumentException("Response content is not a valid json", e);
        }

        var actualJTokens = responseJToken.SelectTokens(jsonPath, false).ToList();
        return actualJTokens;
    }

    private static string AddSquareBrackets(string expected)
    {
        if (!expected.Trim().StartsWith("["))
        {
            expected = expected.Insert(0, "[");
        }

        if (!expected.Trim().EndsWith("]"))
        {
            expected = expected.Insert(expected.Length, "]");
        }

        return expected;
    }

    private static void FailJTokensAssertion(List<JToken> actualJTokens, List<JToken> expectedJTokens,
        string? message = null)
    {
        var actualJson = JsonConvert.SerializeObject(actualJTokens);
        var expectedJson = JsonConvert.SerializeObject(expectedJTokens);
        message = string.IsNullOrEmpty(message) ? message : message + Environment.NewLine;
        Assert.Fail($"{message}Actual:   {actualJson}{Environment.NewLine}Expected: {expectedJson}");
    }

    private List<JToken> GetExpectedJTokensFromFile(string filePath)
    {
        var expectedString = File.ReadAllText(filePath);

        JToken responseJToken;
        try
        {
            responseJToken = JToken.Parse(expectedString);
        }
        catch (JsonReaderException e)
        {
            throw new ArgumentException($"File {filePath} is not a valid json", e);
        }

        return responseJToken.ToList();
    }
}