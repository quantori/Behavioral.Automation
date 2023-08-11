Feature: Third requirement (User can see error notification when provided input is invalid)
wiki: https://de.wikipedia.org/wiki/Primerdesign
DNA sequencing is used to read DNA (A, C, T, G nucleotides)
PCR is used to amplify little part of DNA (for example, 1000 bps)

 Background:
	Given application URL is opened

 @third_requirement
 Scenario: The DNA template is empty
	 Given user entered " " into "Template" input
 	 When user enters "10" into "Forward primer from" input
	 And user clicks on "Perform specificity check" checkbox
	 And user clicks on "Get primers" button
	 Then label "Error" should have "Exception error: No sequence input was provided ." text

 Scenario: The DNA template is shorter than the specified parameter in "Forward primer from input"
	 Given user entered "ATGCTTACAAGACTAGCCTTGCTAGCAACCGCGGGCTGGGGGGCTAAGGTATCACTCAAGAAGC" into "Template" input
	 When user enters "450" into "Forward primer from" input
	 And user clicks on "Perform specificity check" checkbox
	 And user clicks on "Get primers" button
	 Then label "Error" should have "Exception error: Invalid from coordinate (greater than sequence length) ." text

 Scenario: The DNA template contains special characters
	 Given user entered "&@#:+*(((*^^&" into "Template" input
	 When user enters "2" into "Forward primer from" input
	 And user clicks on "Perform specificity check" checkbox
	 And user clicks on "Get primers" button
	 Then label "Error" should have "Exception error: CFastaReader: Near line 1, there's a line that doesn't look like plausible data, but it's not marked as defline or comment. ." text
