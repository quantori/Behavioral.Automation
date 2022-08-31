Feature: Saucedemo
	
@mytag
Scenario: Login
	Given application URL is opened
	And user entered "standard_user" into "Username"	
	And user entered "secret_sauce" into "Password"	
	When user clicks on "Login button"
	Then the ".*?" should have the following values: