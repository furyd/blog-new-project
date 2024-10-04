Feature: Customers

Returns a OK status

Scenario: Make a Customers request
	Given I am calling the Customers endpoint
	When I make a GET request
	Then I should get a 200 response
