# Behavioral.Automation

Bindings Examples

Attribute Binding

[Given("(.*?) (is|is not|become| become not) (enabled|disabled)")]
[When("(.*?) (is|is not|become| become not) (enabled|disabled)")]
[Then("the (.*?) should (be|be not|become|become not) (enabled|disabled)")]

[Then("the following controls should (be|be not|become|become not) (enabled|disabled):")]
[Given("the following controls (are|are not|become| become not) (enabled|disabled):")]
[When("the following controls (are|are not|become| become not) (enabled|disabled):")]
When the following controls become enabled:
Then the following controls should become enabled:

Click Binding

[Given("user clicks on (.*)")]
[When("user clicks on (.*)")]
When user clicks on "Some text" column header

[Given("user clicks twice on (.*)")]
[When("user clicks twice on (.*)")]
When user clicks twice on "Author" column header

[Given("user clicks three times on (.*)")]
[When("user clicks three times on (.*)")]
When user clicks three times on "Some text" column header

[When("user clicks at (.*) element among (.*)")]
When user clicks at first element among "Search results" grid rows

[When("user clicks twice at (.*) element among (.*)")]
When user clicks twice at first element among "Column" headers

[When("user clicks three times at (.*) element among (.*)")]
When user clicks three times at first element among "Column" headers

[When("user hovers mouse over (.*)")]
When user hovers mouse over <buttonName> <controlType>

Control Scope Selection Binding

[Given("inside (.*?): (.*)")]
[When("inside (.*?): (.*)")]
[Then("inside (.*?): (.*)")]
When inside "Search" paginator: user selects "5" in "Items per page" dropdown

[Given("inside (.*?) the following steps were executed:")]
[When("inside (.*?) the following steps are executed:")]
[Then("inside (.*?) the following steps should be executed:")]
Then inside "Search" paginator the following steps should be executed:

[Given("inside (.*?) of (.*?): (.*)")]
[When("inside (.*?) of (.*?): (.*)")]
[Then("inside (.*?) of (.*?): (.*)")]
When inside "Search" paginator: user selects "5" in "Items per page" dropdown

Debug Binding

[Given("wait")]
[When("wait")]
[Then("wait")]

Downloads Binding

[Then("\"(.*?)\" file should be downloaded")]
Then "ArchiveEntries.xlsx" file should be downloaded

[Then("\"(.*?)\" file should (contain|not contain) the following data:")]
Then "ArchiveEntries.xlsx" file should contain the following data:

Dropdown Binding

[Given("(.*?) selected value is \"(.*)\"")]
[Then("the (.*?) selected value should be \"(.*)\"")]
Then "System" dropdown selected value should be empty

[Then("the (.*?) should have the following values:")]
Then the "System" dropdown should have the following values:

[Then("(.*?) should have the following groups:")]
Then "Properties" autocomplete should have the following groups:

[When("(.*?) (contain|not contain) \"(.*)\"")]
[Then("(.*?) should (contain|not contain) \"(.*)\"")]
Then "Search results" grid should contain <authorResult> in "Author" column

[When("(.*?) (contains|not contains) \"(.*)\" in all elements")]
[Then("(.*?) should (have|not have) \"(.*)\" in all elements")]
When "Properties" autocomplete contains <string> in all elements

[When("user selects \"(.*)\" in (.*)")]
When user selects <propertyValue> in "Properties" autocomplete

[When("user selects multiple entries in (.*):")]
When user selects multiple entries in "Included components" dropdown:

[Given("(.*?) selected value (is|is not|become|become not) empty")]
[Then("(.*?) selected value should (be|be not|become|become not) empty")]
Then "Included components" dropdown selected value should be empty

Input Binding

[When("user enters \"(.*)\" into (.*)")]
When user enters "prj.test@gmail.com" into "Google login" input

[Given("user clears (.*)")]
[When("user clears (.*)")]
When user clears <controlName> input

[When("user enters the following values into the following controls:")]
When user enters the following values into the following controls:

Label Binding

[Given("the (.*?) text (is|is not|become|become not) \"(.*)\"")]
[When("the (.*?) text (is|is not|become|become not) \"(.*)\"")]
[Then("the (.*?) text should (be|be not|become|become not) \"(.*)\"")]
Then the "Error" message text should become "No such property"

[Given("(.*?) (contain|not contain) \"(.*)\" text")]
[Then("(.*?) should (contain|not contain) \"(.*)\" text")]
Then "Entry description" record should contain "melting in sealed platinum ampoules in nitrogen atmosphere." text

[Then("the (.*?) should (have|not have) tooltip with text \"(.*)\"")]
Then the <linkName> <controlType> should have tooltip with text <tooltip>

[Given("(.*?) text (is|is not|become|become not) empty")]
[Then("(.*?) text should (be|be not|become|become not) empty")]
Then the "Error" message text should be <errorMessage>

[Then("the following controls should (be|be not|become|become not) empty:")]
Then the following controls should be empty:

[Given("the following controls (are|are not|become|become not) empty:")]


List Binding

[Given("(.*?) (contain|not contain) the following items:")]
[Then("(.*?) should (contain|not contain) the following items:")]


Navigation Binding

[Given("URL \"(.*)\" is opened")]
[When("user opens URL \"(.*)\"")]
When user opens URL "<pageAddress>"

[Given("application URL is opened")]
[When("user opens application URL")]
When user opens application URL

[Given("relative URL \"(.*)\" is opened")]
[When("user opens relative URL \"(.*)\"")]
[Then("user opens relative URL \"(.*)\"")]
Given relative URL <pageAddress> is opened

[Then("page \"(.*)\" should (be|be not|become|become not) opened")]
[Then("relative URL should (be|be not|become|become not) \"(.*)\"")]
Then relative URL should become "<homePage>

[Given("page (contains|not contains) \"(.*)\" URL")]
[When("page (contains|not contains) \"(.*)\" URL")]
[Then("page (should|should not) contain \"(.*)\" URL")]
[Then("page title should (be|be not|become|become not) \"(.*)\"")]
Then page title should become "<title>"

[Given("user sees opened window (.*) page")][When("user sees opened window (.*) page")]
When user sees opened window Google Login page

Presence Binding

[Then("the following controls should (be|be not|become|become not) visible:")]
Then the following controls should become visible:

[Given("the following controls (are|are not|become|become not) visible:")]

[Given("(.*?) (is|is not|become|become not) visible")]
[When("(.*?) (is|is not|become|become not) visible")]
[Then("(.*?) should (be|be not|become|become not) visible")]

Given "Search results" grid rows become visible
Then "Return" link should become visible

Redirection Binding

[Given("user is redirected to (.*) page")]
[When("user is redirected to (.*) page")]
[Then("user should be redirected to (.*) page")]
When user is redirected to Login page

Table Binding

[Given(@"(.*?) has (\d+) items")]
[When(@"(.*?) has (\d+) items")]
[Then(@"(.*?) should have (\d+) items")]
When "Search results" grid has 5 items

[Given("(.*?) (contain|not contain) the following rows:")]
[Then("(.*?) should (contain|not contain) the following rows:")]
[Then("(.*?) should (contain|not contain) \"(.*)\" in (.*)")]
Then "Search results" grid should contain the following rows:

[Then("(.*?) values should be (lesser|greater) than \"(.*)\"")]
Then "Year" column values should be greater than <value>

[Then("(.*?) values should be (between|not between) \"(.*)\" and \"(.*)\"")]
Then "Year" column values should be between <yearFromValue> and <yearToValue>

[Given("(.*?) contain no records in (.*)")]
[Then("(.*?) should contain no records in (.*)")]
Then "Search results" grid should contain no records in "Author" column

[Given("(.*?) has no records")]
[Then("(.*?) should contain no records")]
Then "Search results" grid should contain no records

[Given("(.*?) element among (.*) (is| is not) expanded")]
[When("(.*?) element among (.*) (is| is not) expanded")]
[Then("(.*?) element among (.*) should (be|be not) expanded")]
When first element among "Search results" grid rows is expanded
