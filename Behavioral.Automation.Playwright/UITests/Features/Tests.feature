Feature: Tests for Behavioral.Automation.Playwright project

    Background:
        Given application URL is opened
        And page title is "Weather forecast"
        Then the "Demo label" should be visible        

    Scenario: Counter clicks test
        When user clicks on "Click me"
        And user waits for 2 seconds
        And user clicks twice on "Click me"
        And user waits for 2 seconds
        Then the Counter text should contain "30"

    Scenario: Table checks
        Given URL "/fetchdata" is opened
        And the Page header text is "Weather forecast"
        And the "Forecast table" is enabled
        And the "Forecast table" contains the following rows with headers:
          | Date | Temp. (C) | Temp. (F) | Summary |