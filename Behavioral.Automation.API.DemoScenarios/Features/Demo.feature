Feature: reqres test

  Scenario: Get all users
    When user sends a "GET" request to "api/users" url
    Then response json path "$..data[?(@.email == 'george.bluth@reqres.in')].first_name" value should be "["George"]"

  Scenario: Get second page
    When user sends a "GET" request to "api/users" url with the following parameters:
      | Name         | Value | Kind   |
      | page         | 2     | Param  |
      | CustomHeader | test  | Header |

  Scenario: Complex request
    Given user creates a "POST" request to "api/users" url with the json:
    """
    {
    "name": "morpheus",
    "job": "leader"
    }
    """
    When user adds parameters and send request:
      | Name         | Value | Kind   |
      | CustomHeader | test  | Header |
      
  Scenario: Request with json file
    When user sends a "POST" request to "api/users" url with the json file "TestData\User.json"