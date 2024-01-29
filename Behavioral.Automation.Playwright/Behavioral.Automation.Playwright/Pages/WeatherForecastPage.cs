using Behavioral.Automation.Configs;
using Behavioral.Automation.Playwright.Configs;
using Behavioral.Automation.Playwright.Pages;
using Behavioral.Automation.Playwright.Services.ElementSelectors;

public class WeatherForecastPage : ISelectorStorage
{
    private static readonly string Id = ConfigManager.GetConfig<Config>().SearchAttribute;

    public TableSelector ForecastTable = new()
    {
        IdSelector = "weather-forecast-table",
        BaseElementSelector = new ElementSelector() { IdSelector = "weather-forecast-table" },
        RowSelector = new ElementSelector() {XpathSelector = "//table[@class='table']/tbody/tr"},
        HeaderCellSelector = new ElementSelector() {XpathSelector = "//table[@class='table']/thead/tr/th"}
    };

    public ElementSelector PageHeader = new() { IdSelector = "weather-forecast-header" };
}