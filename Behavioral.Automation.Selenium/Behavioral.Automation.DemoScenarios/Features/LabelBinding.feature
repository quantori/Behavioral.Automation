Feature: LabelBinding

@Automated
Scenario: Label should contain expected text
	Given application URL is opened
	Then "Main page" header should become visible
	And the "Main page" header text should be "Behavioral automation demo"
	
Scenario: User input something
    Given application URL is opened
    When user clicks on "Greetings" link
    And user enters "Vlodimir" into "Name" field
    And user clicks on "Say hello" button
    Then "Hello" text should become visible
    And "Hello" text should contain "Hello, Vlodimir!" text