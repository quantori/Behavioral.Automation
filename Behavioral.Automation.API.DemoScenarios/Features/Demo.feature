Feature: reqres test

Scenario: Get all users
	When user sends a "GET" request to "api/users" url
	Then response json path "$..data[?(@.email == 'george.bluth@reqres.in')].first_name" value should be "["George"]"