using Behavioral.Automation.API.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace Behavioral.Automation.API.Bindings;

[Binding]
public class SaveToContextSteps
{
    private readonly ApiContext _apiContext;
    private readonly ScenarioContext _scenarioContext;
    private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
    
    public SaveToContextSteps(ApiContext apiContext, ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
    {
        _apiContext = apiContext;
        _scenarioContext = scenarioContext;
        _specFlowOutputHelper = specFlowOutputHelper;
    }

    [Given("save response json path \"(.*)\" as \"(.*)\"")]
    [When("save response json path \"(.*)\" as \"(.*)\"")]
    public void SaveResponseJsonPathAs(string jsonPath, string variableName)
    {
        var stringToSave = GetStringByJsonPath(jsonPath);
        _scenarioContext.Add(variableName, stringToSave);
        _specFlowOutputHelper.WriteLine($"Saved '{stringToSave}' with key '{variableName}' in scenario context");
    }

    private string GetStringByJsonPath(string jsonPath)
    {
        var actualJTokens = GetActualJTokensFromResponse(jsonPath);
        var stringToSave = ConvertJTokensToString(actualJTokens);
        if (stringToSave.Equals("[]"))
        {
            Assert.Fail($"Empty value by jsonpath {jsonPath}. Can't save empty string in scenario context.");
        }
        return stringToSave;
    }

    private string ConvertJTokensToString(List<JToken> tokens)
    {
        return tokens.Count == 1 ? tokens[0].ToString() : JsonConvert.SerializeObject(tokens);
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
        if (actualJTokens == null)
        {
            Assert.Fail($"No value by json path: {jsonPath}");
        }
        return actualJTokens;
    }
}