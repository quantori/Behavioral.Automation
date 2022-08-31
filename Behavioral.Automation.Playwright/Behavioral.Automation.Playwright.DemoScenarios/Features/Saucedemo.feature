Feature: Saucedemo

  Scenario: Purchase
    Given application URL is opened
    And user entered "standard_user" into "Username"
    And user entered "secret_sauce" into "Password"
    When user clicks on "Login button"
    And user clicks on "Backpack"
    Then the "Shopping cart badge" text should be "1"