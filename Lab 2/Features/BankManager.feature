@setup_feature
Feature: Bank Manager Customer Sorting
  Scenario: Sort customer names by first name
    Given I am on the login page
    When I click on the "Bank Manager Login" button
    And I click on the "Customers" button
    Then the customer names should be sorted by first name when I click the "First Name" sort button