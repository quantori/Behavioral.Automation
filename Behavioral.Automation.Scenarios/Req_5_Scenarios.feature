Feature: Fifth requirement (User can specify exon-exon junction attribute, start design and see primers pairs and attributes for designed primers)
wiki: https://de.wikipedia.org/wiki/Primerdesign
DNA sequencing is used to read DNA (A, C, T, G nucleotides)
PCR is used to amplify little part of DNA (for example, 1000 bps)

 Background:
    Given application URL is opened

 @fifth_requirement
 Scenario: User can provide DNA template accession number and specify exon-exon junction attribute
    Given user entered "NM_000115.3" into "Template" input
    When user opens "Exon junction span" dropdown and selects "Primer must span an exon-exon junction" in dropdown menu
    And user clicks on "Perform specificity check" checkbox
    And user clicks on "Get primers" button
    Then "Primers design" table should become visible
    And "Primers design" table should have the following rows:
      | Sequence (5'->3')     | Length | Tm    |
      | TCACCTAAAGCAGAGACGGG  | 20     | 59.10 |

 Scenario: User can provide DNA template accession number and specify exon-exon junction attribute with settings
    Given user entered "NM_000115.3" into "Template" input
    When user opens "Exon junction span" dropdown and selects "Primer must span an exon-exon junction" in dropdown menu
    And user enters "75" into "Minimal size of PCR product" input
    And user enters "5" into "Min Site overlap by three prime end" input
    And user enters "8" into "Min Site overlap by five prime end" input
    And user enters "9" into "Max Site overlap by three prime end" input
    And user clicks on "Perform specificity check" checkbox
    And user clicks on "Get primers" button
    Then "Primers design" table should become visible
    And "Primers design" table should have the following rows:
      | Sequence (5'->3')    | Length | Self complementarity |
      | TCACCTAAAGCAGAGACGGG | 20     | 	2.00             |
