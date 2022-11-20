using Behavioral.Automation.Transformations.ScribanFunctions;
using Scriban;
using Scriban.Runtime;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace Behavioral.Automation.Transformations;

[Binding]
public class Transformations
{
    private readonly ConfigFunction _configFunction;
    private readonly ScenarioContext _scenarioContext;
    private static readonly List<IScriptObject> customFunctions = new List<IScriptObject>();
    private readonly SpecFlowOutputHelper _specFlowOutputHelper;

    public Transformations(ConfigFunction configFunction, ScenarioContext scenarioContext, SpecFlowOutputHelper specFlowOutputHelper)
    {
        _configFunction = configFunction;
        _scenarioContext = scenarioContext;
        _specFlowOutputHelper = specFlowOutputHelper;
    }

    public static void AddFunction(IScriptObject function)
    {
        customFunctions.Add(function);
    }

    [StepArgumentTransformation]
    public string ProcessStringTransformations(string value)
    {
        var template = Template.Parse(value);
        var result = template.Render(GetContext());
        return result;
    }

    [StepArgumentTransformation]
    public Table ProcessTableTransformations(Table table)
    {
        var context = GetContext();
        var newTable = table;
        if (table.Rows.Count > 0)
        {
            foreach (var row in newTable.Rows)
            {
                foreach (var value in row)
                {
                    var template = Template.Parse(value.Value);
                    var result = template.Render(context);
                    row[value.Key] = result;
                }
            }
        }

        return newTable;
    }

    [Given("save variable \"(.*)\" with value:")]
    [Given("save variable \"(.*)\" with value \"(.*)\"")]
    public void SaveStringIntoContext(string variableName, string stringToSave)
    {
        _scenarioContext.Add(variableName, stringToSave);
        _specFlowOutputHelper.WriteLine($"Saved '{stringToSave}' with key '{variableName}' in scenario context");
    }

    private TemplateContext GetContext()
    {
        var specflowContextVariables = new ScriptObject();
        if (_scenarioContext.Any())
        {
            foreach (var item in _scenarioContext)
            {
                specflowContextVariables.Add(item.Key, item.Value);
            }
        }

        var context = new TemplateContext();

        context.PushGlobal(specflowContextVariables);
        context.PushGlobal(_configFunction);
        foreach (var customFunction in customFunctions)
        {
            context.PushGlobal(customFunction);
        }

        return context;
    }
}