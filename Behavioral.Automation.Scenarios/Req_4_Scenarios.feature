Feature: Fourth requirement (User can provide primers, start design and see primers pairs and attributes for designed primers)
wiki: https://de.wikipedia.org/wiki/Primerdesign
DNA sequencing is used to read DNA (A, C, T, G nucleotides)
PCR is used to amplify little part of DNA (for example, 1000 bps)

 Background:
    Given application URL is opened

 @fourth_requirement
 Scenario: User can provide DNA template accession number and forward primer
     Given user entered "T57453" into "Template" input
     When user enters "GAGAAGACATACCTGCCCGC" into "Forward primer" input
     And user clicks on "Perform specificity check" checkbox
     And user clicks on "Get primers" button
     Then "Primers design" table should become visible

 Scenario: User can provide DNA template accession number and reverse primer
      Given user entered "T57453" into "Template" input
      When user enters "TCCTCTCCCTTCAGCACAGA" into "Reverse primer" input
      And user clicks on "Perform specificity check" checkbox
      And user clicks on "Get primers" button
      Then "Primers design" table should become visible
    
 Scenario: User can provide DNA template accession number and primers
       Given user entered "T57453" into "Template" input
       When user enters "ACTATGGGCACACGACTCC" into "Forward primer" input
       And user enters "GAGATTCCTTCACCAAATCCCA" into "Reverse primer" input
       And user clicks on "Perform specificity check" checkbox
       And user clicks on "Get primers" button
       Then "Primers design" table should become visible
 
 Scenario: User can provide DNA template accession number and primers with settings
       Given user entered "T57453" into "Template" input
       When user enters "ACTATGGGCACACGACTCC" into "Forward primer" input
       And user enters "GAGATTCCTTCACCAAATCCCA" into "Reverse primer" input
       And user enters "130" into "Forward primer to" input
       And user enters "80" into "Minimal size of PCR product" input
       And user enters "5" into "Min Site overlap by three prime end" input
       And user clicks on "Perform specificity check" checkbox
       And user clicks on "Get primers" button
       Then "Primers design" table should become visible