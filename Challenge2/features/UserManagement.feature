Feature: User Management

  Scenario: Register a new user
    Given I have the following user details
      | name     | password         | email             | countryCode | phoneNumber | address         |
      | John Doe | Secretpassword3* | john@yopmail.com  | US          | 2154567890  | 123 Main Street |
    When I register the user
    Then the user should be registered successfully

  Scenario: Login user
    Given I have the following login details
      | identifier | password         |
      | 236480     | Secretpassword3* |
    When I login the user
    Then the user should be logged in successfully

  Scenario: Create a PIN
    Given I have the following PIN details
      | accountNumber | pin  | password         |
      | 236480        | 1234 | Secretpassword3* |
    When I create the PIN
    Then the PIN should be created successfully

  Scenario: Deposit funds
    Given I have the following deposit details
      | accountNumber | pin  | amount |
      | 236480        | 1234 | 10.0   |
    When I deposit the funds
    Then the funds should be deposited successfully
