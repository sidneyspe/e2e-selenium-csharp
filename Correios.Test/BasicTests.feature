Feature: BasicTests

A short summary of the feature

@regression
Scenario: App successfully starts
	Then the homepage is loaded


@regression
Scenario: CEP not exist
	Given the user search for CEP "80700000"
	Then the message "CEP não existe" is show

