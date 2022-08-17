Feature: Homework

Scenario Outline: Tab navigation
	Given application URL is opened
	When user navigates to <pageName> by <navigationType>
	Then relative URL should become "<URL>"
	And <element> should become visible
	
	Examples: 
	| pageName  | navigationType | URL        | element           |
	| Greetings | tab click      | /greetings | "Name" field      |
	| Counter   | URL            | /counter   | "Click me" button |
	| Forecast  | tab click      | /fetchdata | "Forecast" table  |