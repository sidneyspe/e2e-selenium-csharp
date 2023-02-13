Feature: BasicTests

A short summary of the feature

@regression
Scenario: App successfully starts
	Then the homepage is loaded


@regression
Scenario: CEP not exist
	Given the user search for CEP "80700000"
	Then the message "Dados não encontrado" is show


@regression
Scenario: CEP exist
	Given the user search for CEP "01013-001"
	Then the message of success "Rua Quinze de Novembro - lado ímpar" is show


@regression
Scenario: Code not correct
	Given the user search for code "SS987654321BR"
	Then the message of code "Rastreamento" is show


@regression
Scenario: Full Scenario
	Then the homepage is loaded
	Given the user search for CEP "80700000"
	Then the message "Dados não encontrado" is show
	And click on homepage
	Given the user search for CEP "01013-001"
	Then the message of success "Rua Quinze de Novembro - lado ímpar" is show
	And click on homepage
	Given the user search for code "SS987654321BR"
	Then the message of code "Rastreamento" is show
	