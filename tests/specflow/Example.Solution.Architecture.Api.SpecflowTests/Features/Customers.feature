Feature: Customers

Actions on the Customers entity

Scenario: List all cutomers
	Given I am calling the Customers list endpoint
	When I make a GET request
	Then I should get either a 200 or 204 response

Scenario: Get a customer by ID
	Given I am calling the Customers get endpoint with the ID 00000000-0000-0000-0000-000000000000
	When I make a GET request
	Then I should get a 404 response

Scenario: Create a new customer
	Given I am calling the Customers create endpoint
	When I make a POST request with the following data
		| GivenName | FamilyName |
		| A | B |
	Then I should get a 201 response

Scenario: Update a customer
	Given I am calling the Customers update endpoint with the ID 00000000-0000-0000-0000-000000000000
	When I make a PUT request with the following data
		| GivenName | FamilyName |
		| A | B |
	Then I should get a 404 response

Scenario: Delete a customer
	Given I am calling the Customers delete endpoint with the ID 00000000-0000-0000-0000-000000000000
	When I make a DELETE request
	Then I should get a 404 response
