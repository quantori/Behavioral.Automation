Feature: Button step definition tests
  
  Scenario: Check button click
    Given application URL is opened
    When user clicks the "Increment count" button

  Scenario: Visibility binding check
    Given application URL is opened
    Then the "Demo label" should be visible