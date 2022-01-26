Feature: ExampleBinding

@Automated
Scenario: Open Google page
	When user opens URL "https://www.google.com/"
	Then page title should become "Google"
	
