Feature: LabelBinding

@Automated
Scenario: Label should contain expected text
	Given application URL is opened
	Then the "Demo" label should become visible
	And the "Demo" label text should be "Behavioral automation demo"

	
