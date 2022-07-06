Feature: Examples

@Automated
Scenario: Open Google page
	When user opens URL "https://www.google.com/"
	Then page title should become "Google"
	
@Automated
Scenario: Find something on Wikipedia
	When user opens URL "https://en.wikipedia.org/"
	And user enters "French bulldog" into "Search" input
	And user clicks on "Magnifying glass" button
	Then the "Page header" text should become "French Bulldog"