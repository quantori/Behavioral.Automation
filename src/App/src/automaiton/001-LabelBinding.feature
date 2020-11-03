Feature: 001 Label Binding

  @Automated
  @NoAuthentication
  Scenario: 001-01 User goes to main page
    Then "Simple text" is visible
    And "Simple text" label text should become "Behavioral automation demo"