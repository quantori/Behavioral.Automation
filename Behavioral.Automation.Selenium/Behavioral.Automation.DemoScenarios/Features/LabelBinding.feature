Feature: LabelBinding

@Automated
Scenario: Label should contain expected text
	Given application URL is opened
	Then "Main page" header should become visible
	And the "Main page" header text should be "Behavioral automation demo"