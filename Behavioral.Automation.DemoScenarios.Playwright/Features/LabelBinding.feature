Feature: LabelBinding

@Automated
Scenario: Label should contain expected text
	Given application URL is opened
	Then the "Demo" label text should become "Behavioral automation demo"
	
