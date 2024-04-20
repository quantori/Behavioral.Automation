Feature: Tests for Bahavioral.Automation.Playwright project
  
  Scenario: Check button click
    Given application URL is opened
    And the "Count quantity" label text is "Current count: 0"
    When user clicks the "Increment count" button
    Then the "Count quantity" label text should become "Current count: 1"

  Scenario: Visibility binding check
    Given application URL is opened
    Then the "Demo label" should be visible