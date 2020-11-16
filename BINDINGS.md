# Bindings Examples

## Attribute Binding

 * Check if a single element is enabled or disabled:
[Given("(.*?) (is|is not|become| become not) (enabled|disabled)")]
[When("(.*?) (is|is not|become| become not) (enabled|disabled)")]
[Then("the (.*?) should (be|be not|become|become not) (enabled|disabled)")]

* Check if a set of elements are enabled or disabled:
[Then("the following controls should (be|be not|become|become not) (enabled|disabled):")]
[Given("the following controls (are|are not|become| become not) (enabled|disabled):")]
[When("the following controls (are|are not|become| become not) (enabled|disabled):")]
Examples:
When the following controls become enabled:
Then the following controls should become enabled:

## Click Binding

* Execute click on the element:
[Given("user clicks on (.*)")]
[When("user clicks on (.*)")]
Example:
When user clicks on "Some text" column header

* Execute double click on the element:
[Given("user clicks twice on (.*)")]
[When("user clicks twice on (.*)")]
Example:
When user clicks twice on "Author" column header

* Execute triple click on the element:
[Given("user clicks three times on (.*)")]
[When("user clicks three times on (.*)")]
Example:
When user clicks three times on "Some text" column header

* Execute click on the specific element among multiple elements by its number:
[When("user clicks at (.*) element among (.*)")]
Example:
When user clicks at first element among "Search results" grid rows

* Execute double click on the specific element among multiple elements by its number:
[When("user clicks twice at (.*) element among (.*)")]
Example:
When user clicks twice at first element among "Column" headers

* Execute triple click on the specific element among multiple elements by its number:
[When("user clicks three times at (.*) element among (.*)")]
Example:
When user clicks three times at first element among "Column" headers

* Hover mouse over element:
[When("user hovers mouse over (.*)")]
Example:
When user hovers mouse over <buttonName> <controlType>

## Control Scope Selection Binding

* Execute action inside control scope:
[Given("inside (.+?): (.+?)")]
[When("inside (.+?): (.+?)")]
[Then("inside (.+?): (.+?)")]
Example:
When inside "Search" paginator: user selects "5" in "Items per page" dropdown

* Execute multiple actions inside control scope:
[Given("inside (.+?) the following steps were executed:")]
[When("inside (.+?) the following steps are executed:")]
[Then("inside (.+?) the following conditions should be true:")]
Example:
Then inside "Search" paginator the following steps should be executed:

* Execute action inside parent control:
[Given("inside (.+?) of (.+?): (.+?)")]
[When("inside (.+?) of (.+?): (.+?)")]
[Then("inside (.+?) of (.+?): (.+?)")]
Example:
When inside "Search" paginator: user selects "5" in "Items per page" dropdown

## Debug Binding
* Freeze execution:
[Given("wait")]
[When("wait")]
[Then("wait")]

## Downloads Binding
* Check if file has been downloaded:
[Then("\"(.*?)\" file should be downloaded")]
Example:
Then "ArchiveEntries.xlsx" file should be downloaded

* Check that Excel file contains data given in the Specflow table:
[Then("\"(.*?)\" file should (contain|not contain) the following data:")]
Example:
Then "ArchiveEntries.xlsx" file should contain the following data:

## Dropdown Binding
* Check value selected in the dropdown:
[Given("(.*?) selected value is \"(.*)\"")]
[Then("the (.*?) selected value should be \"(.*)\"")]
Example:
Then "System" dropdown selected value should be empty

* Check values which are available in the dropdown:
[Then("the (.*?) should have the following values:")]
Example:
Then the "System" dropdown should have the following values:

* Check values headers inside the dropdown:
[Then("(.*?) should have the following groups:")]
Example:
Then "Properties" autocomplete should have the following groups:

* Check if dropdown contains particular value:
[When("(.*?) (contain|not contain) \"(.*)\"")]
[Then("(.*?) should (contain|not contain) \"(.*)\"")]
Example:
Then "System" grid should contain <value>

* Check that all values in dropdown contain substring:
[When("(.*?) (contains|not contains) \"(.*)\" in all elements")]
[Then("(.*?) should (have|not have) \"(.*)\" in all elements")]
Example:
When "Properties" autocomplete contains <string> in all elements

* Select value inside dropdown:
[Given("user selected \"(.*?)\" in (.*?)")]
[When("user selects \"(.*?)\" in (.*?)")]
Example:
When user selects <propertyValue> in "Properties" autocomplete

* Select multiple entries in dropdown with multiple choice:
[Given("user selected multiple entries in (.*?):")]
[When("user selects multiple entries in (.*?):")]
Example:
When user selects multiple entries in "Included components" dropdown:

* Check that dropdown has no value selected:
[Given("(.*?) selected value (is|is not|become|become not) empty")]
[Then("(.*?) selected value should (be|be not|become|become not) empty")]
Example:
Then "Included components" dropdown selected value should be empty

## Input Binding

* Input string value into element:
[When("user enters \"(.*)\" into (.*)")]
Example:
When user enters "prj.test@gmail.com" into "Google login" input
When user enters "test-__random_string:4" into "Username" input

Where:

__random_string - default 8 symbols

__random_string:N - N symbols

* Clear value stored in element:
[Given("user clears (.*)")]
[When("user clears (.*)")]
Example:
When user clears <controlName> input

* Fill multiple elements with mupltiple values:
[When("user enters the following values into the following controls:")]
Example:
When user enters the following values into the following controls:

## Label Binding

* Check that element text is equal to given value:
[Given("the (.*?) text (is|is not|become|become not) \"(.*)\"")]
[When("the (.*?) text (is|is not|become|become not) \"(.*)\"")]
[Then("the (.*?) text should (be|be not|become|become not) \"(.*)\"")]
Example:
Then the "Error" message text should become "No such property"

* Check that element text contains given value:
[Given("(.*?) (contain|not contain) \"(.*)\" text")]
[Then("(.*?) should (contain|not contain) \"(.*)\" text")]
Example:
Then "Entry description" record should contain "some string" text

* Check that element has a tooltip with given value:
[Then("the (.*?) should (have|not have) tooltip with text \"(.*)\"")]
Example:
Then the <linkName> <controlType> should have tooltip with text <tooltip>

* Check that element's text is empty:
[Given("(.*?) text (is|is not|become|become not) empty")]
[Then("(.*?) text should (be|be not|become|become not) empty")]
Example:
Then the "Error" message text should be empty

* Check that multiple elements' texts are empty
[Given("the following controls (are|are not|become|become not) empty:")]
[Then("the following controls should (be|be not|become|become not) empty:")]
Example:
Then the following controls should be empty:

## List Binding

* Check that list has given values
[Given("(.+?) (has|does not have) the following items:")]
[Then("(.+?) should (have|not have) the following items:")]
Example:
Then "Categories" should have the following items:

* Check that list has given values in exact order
[Then("(.+?) should have in exact order the following items:")]
Example:
Then "Categories" should have in exact order the following items:

* Check that list contains given values
[Given("(.+?) (has|does not have) the following items:")]
[Then("(.+?) should (have|not have) the following items:")]
Example:
Then "Categories" list should contain the following items:


## Navigation Binding

* Open URL: 
[Given("URL \"(.*)\" is opened")]
[When("user opens URL \"(.*)\"")]
Example:
When user opens URL "<pageAddress>"

* Open URL stated in config as base URL:
[Given("application URL is opened")]
[When("user opens application URL")]
Example:
When user opens application URL

* Open relative URL:
[Given("relative URL \"(.*)\" is opened")]
[When("user opens relative URL \"(.*)\"")]
[Then("user opens relative URL \"(.*)\"")]
Example:
Given relative URL "/test-url" is opened

* Check that relative URL is opened:
[Then("page \"(.*)\" should (be|be not|become|become not) opened")]
[Then("relative URL should (be|be not|become|become not) \"(.*)\"")]
Example:
Then relative URL should become "/url"

* Check that page's URL contains given value:
[Given("page (contains|not contains) \"(.*)\" URL")]
[When("page (contains|not contains) \"(.*)\" URL")]
[Then("page (should|should not) contain \"(.*)\" URL")]
Example:
Then page title should become "<title>"

* Check page title:
[Then("page title should (be|be not|become|become not) \"(.*)\"")]

* Check if popup window is opened:
[Given("user sees opened window (.*) page")]
[When("user sees opened window (.*) page")]
Example:
When user sees opened window Google Login page

## Presence Binding

* Check that multiple elements are visible:
[Given("the following controls (are|are not|become|become not) visible:")]
[Then("the following controls should (be|be not|become|become not) visible:")]
Example:
Then the following controls should become visible:

* Check that element is visible:
[Given("(.*?) (is|is not|become|become not) visible")]
[When("(.*?) (is|is not|become|become not) visible")]
[Then("(.*?) should (be|be not|become|become not) visible")]
Example:
Given "Search results" grid rows become visible
Then "Return" link should become visible

## Redirection Binding

* Change scope if relative URL is not suitable to use:
[Given("user is redirected to (.*) page")]
[When("user is redirected to (.*) page")]
[Then("user should be redirected to (.*) page")]
Example:
When user is redirected to Login page

## Table Binding

* Check number of items in the grid or table:
[Given(@"(.*?) has (\d+) items")]
[When(@"(.*?) has (\d+) items")]
[Then(@"(.*?) should have (\d+) items")]
Example:
When "Search results" grid has 5 items

* Check that grid or table has given rows:
[Given("(.+?) (has|does not have) the following rows:")]
[Then("(.+?) should (have|not have) the following rows:")]
Example:
Then "Search results" grid should have the following rows:

[Then("(.+?) should have in exact order the following rows:")]
Example:
Then "Search results" grid should have in exact order the following rows:

* Check that grid or table contains given rows:
[Given("(.+?) (contains|does not contain) the following rows:")]
[Then("(.+?) should (contain|not contain) the following rows:")]
Example:
Then "Search results" grid should contain the following rows:

* Check that numerical values in the table column are lesser or greater than the given value: 
[Then("(.*?) values should be (lesser|greater) than \"(.*)\"")]
Example:
Then "Year" column values should be greater than <value>

* Check that numerical values in the table column are between given boundaries: 
[Then("(.*?) values should be (between|not between) \"(.*)\" and \"(.*)\"")]
Example:
Then "Year" column values should be between <yearFromValue> and <yearToValue>

* Check that table contains no records in given column:
[Given("(.*?) contain no records in (.*)")]
[Then("(.*?) should contain no records in (.*)")]
Example:
Then "Search results" grid should contain no records in "Author" column

* Check that table is empty:
[Given("(.*?) has no records")]
[Then("(.*?) should contain no records")]
Example:
Then "Search results" grid should contain no records

* Check that table row is expanded:
[Given("(.*?) element among (.*) (is| is not) expanded")]
[When("(.*?) element among (.*) (is| is not) expanded")]
[Then("(.*?) element among (.*) should (be|be not) expanded")]
Example:
When first element among "Search results" grid rows is expanded