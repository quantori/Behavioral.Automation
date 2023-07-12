Feature: Design primer for PCR
	primer blast application - https://www.ncbi.nlm.nih.gov/tools/primer-blast/
	Human DNA is 3 billion bases chain of A/C/T/G nucleotides
	Region of interest usually about 1000 bp
	To amplify this region we need to perform PCR
	
Scenario: PCR primers can be designed
	Given application URL is opened
	And "Perform specificity check" checkbox is unchecked
	When user clicks on "Get primers" button
	Then "Primers design" table should become visible