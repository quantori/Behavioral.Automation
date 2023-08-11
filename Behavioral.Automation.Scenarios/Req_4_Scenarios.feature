Feature: Fourth requirement (User can provide primers, start design and see primers pairs and attributes for designed primers)
wiki: https://de.wikipedia.org/wiki/Primerdesign
DNA sequencing is used to read DNA (A, C, T, G nucleotides)
PCR is used to amplify little part of DNA (for example, 1000 bps)

 Background:
    Given application URL is opened

 @fourth_requirement
 Scenario: User can provide DNA template accession number and forward primer
     Given user entered "U49442.1" into "Template" input
     When user enters "CGGGTCAGGAATGCAGCTAA" into "Forward primer" input
     And user clicks on "Perform specificity check" checkbox
     And user clicks on "Get primers" button
     Then "Primers design" table should become visible
     And "Primers design" table should have the following rows:
       | Sequence (5'->3')    | Length | Tm    |
       | ATTCCATCGAGGGTCACTGC | 20     | 59.82 |

 Scenario: User can provide DNA template accession number and reverse primer
      Given user entered "U49442.1" into "Template" input
      When user enters "CTTGGCTCCGCGTAAAAGTG" into "Reverse primer" input
      And user clicks on "Perform specificity check" checkbox
      And user clicks on "Get primers" button
      Then "Primers design" table should become visible
      And "Primers design" table should have the following rows:
        | Sequence (5'->3')    | Length | Tm    |
        | CGGGTCAGGAATGCAGCTAA | 20     | 60.11 |

    
 Scenario: User can provide DNA template accession number and primers
       Given user entered "U49442.1" into "Template" input
       When user enters "CGGGTCAGGAATGCAGCTAA" into "Forward primer" input
       And user enters "CTTGGCTCCGCGTAAAAGTG" into "Reverse primer" input
       And user clicks on "Perform specificity check" checkbox
       And user clicks on "Get primers" button
       Then "Primers design" table should become visible
       And "Primers design" table should have the following rows:
         | Sequence (5'->3')    | Length | Tm    |
         | CGGGTCAGGAATGCAGCTAA | 20     | 60.11 |
 
 Scenario: User can provide DNA template accession number and primers with settings
       Given user entered "U49442.1" into "Template" input
       When user enters "CGGGTCAGGAATGCAGCTAA" into "Forward primer" input
       And user enters "CTTGGCTCCGCGTAAAAGTG" into "Reverse primer" input
       And user enters "10" into "Forward primer from" input
       And user enters "80" into "Minimal size of PCR product" input
       And user enters "5" into "Min Site overlap by three prime end" input
       And user clicks on "Perform specificity check" checkbox
       And user clicks on "Get primers" button
       Then "Primers design" table should become visible
       And "Primers design" table should have the following rows:
         | Sequence (5'->3')    | Length | GC%   |
         | CGGGTCAGGAATGCAGCTAA | 20     | 55.00 |