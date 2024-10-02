Feature: HelloWorld

Returns a OK status with "hello world"

Scenario: Make a Hello World request
	Given I am calling the Hello World endpoint
	When I make a GET request
	Then I should get a 200 response
