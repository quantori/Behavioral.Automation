﻿Feature: TestCases
	Test Cases for Primer Blast application
    
  Scenario: Req1_User can start primers design 
    Given application URL is opened
    And user entered "GTCAAGCCAGTCACGCAGTAACGTTCATCAGCTAACGTAACAGTTAGAGGCTCGCTAAATCGCACTGTCGGCGTCCCTTGGGTATTTTACGCTAGCATCAGGTAGGCTAGCATGTATCTTTCCTCCCAGGGGTATGCGGGTGCGTGGACAAATGAGCAGCAAACGTAAATTCTCGGCGTGCTTGGTGTCTCGTATTTCTCCTGGAGATCAAGGAAATGTTTCATGACCAAGCGAAAGGCCGCTCTACGGAATGGATTTACGTTACTGCCTGCATAAGGAGACCGGTGTAGCCAAGGACGAAGGCGACCCTAGGTTCTAACCGTCGACTTCGGCGGTAAGGTATCACTCAGGAAGCAGACACTGATAGACACGGTCTAGCAGATCGTTTGACGACTAGGTCAAATTGAGTGGTTTAATATCGGCATGTCTGGCTTTAGAATTCAGTATAGTGCGCTGATCCGAGTCGAATTAAAAACACCAGTACCCAAAACCAGGCGGGCTCGCCACGTCGGCTAATCCTGGTACATTTTGTAAACAATGTTCTGAAGAAAATTTGTGAAAGAAGGACGGGTCATCGCCTACTAATAGCAACAACGATCGGCCGCACCTTCCATTGTCGTGGCGACGCTCGGATTACACGGCAAAGGTGCTTGTGTTCCGACAGGCTAGCATATAATCCTGAGGCGTTACCCCAATCGTTCACCGTCGGATTTGCTACAGCCCCTGAACGCTACATGTACGAAACCATGTTATGTATGCACTAGGCCAACAATAGGACGTAGCCTTGTAGTTAGTACGTAGCCTGGTCGCATAAGTACAGTAGATCCTCCCCGCGCATCCTATTTATTAAGTTAATTCTACAGCAAAACGATCATATGCAGATCCGCAGTGGCCGGTAGACACACGTCCACCCCGCTGCTCTGTGACAGGGACTAAAGAGGCGAAGATTATCGTGTGTGCCCCGTTATGGTCGAGTTCGGTCAGAGCGTCATTGCGAGTAGTCGTTTGCTTTCTCGAATTCCGAGCGATTAAGCGTGACAGTCCCAGCGAACCCACAAAACGTGATCGCAGTCCATGCGATCATACGCAAGAAGGAAGGTCCCCATACACCGACGCACCAGTTTACACGCCGTATGCATAAACGAGCTGCACAAACGAGAGTGCTTGAACTGGACCTCTAGTTCCTCTACAAAGAACAGGTTGACCTGTCGCGAAGTTGCCTTGCCTAGATGCAATGTCGGACGTATTACTTTTGCCTCAACGGCTCCTGCTTTCGCTGAAACCCAAGACAGGCAACAGTAACCGCCTTTTGAAGGCGAGTCCTTCGTCTGTGACTAACTGTGCCAAATCGTCTTCCAAACTCCTAATCCAGTTTAACTCACCAAATTATAGCCATACAGACCCTAATTTCATATCATATCACGCCATTAGCCTCTGCTAAAATTCTGTGCTCAAGGGTTTTGGTTCGCCCGAGTGATGTTGCCAATTAGGACCATCAAATGCACATGTTACAGGACTTCTTATAAATACTTTTTTCCTGGGGAGTAGCGGATCTTAATGGATGTTGCCAGCTGGTATGGAAGCTAATAGCGCCGGTGGGAGCGTAATCTGCCGTCTCCACCAACACAACGCTATCGGGTCATATTATAAGATTCCGCAATGGGGTTACTTATAGGTAGCCTTAACGATATCCGGAACTTGCGATGTACGTGCTATGCTTTAATACATACCTGGCCCAGTAGTTTTCCAATATGGGAACATCAATTGTACATCGGGCCGGGATAATCATGTCATCACGGAAGTAGCCGTAAGACAAATAATTCAAAAGAGATGTCGTTTTGCTAGTTCACGTGAAGGTGTCTCGCGCCACCTCTAAGTAAGTGGGCCGTCGAGACATTATCCCTGATTTTTTCACTACTATTAGTACTCACGGCGCAATACCACCACAGCCTTGTCTCGCCAGAATGCTGGTCAGCATACGGAAGAGCTCAAGGCAGGTC" into "Template" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then page title should become "Primer-Blast results"
    
  Scenario: Req1_User can start primers design and specify forward primer parameters
    Given application URL is opened
    And user entered "GTCAAGCCAGTCACGCAGTAACGTTCATCAGCTAACGTAACAGTTAGAGGCTCGCTAAATCGCACTGTCGGCGTCCCTTGGGTATTTTACGCTAGCATCAGGTAGGCTAGCATGTATCTTTCCTCCCAGGGGTATGCGGGTGCGTGGACAAATGAGCAGCAAACGTAAATTCTCGGCGTGCTTGGTGTCTCGTATTTCTCCTGGAGATCAAGGAAATGTTTCATGACCAAGCGAAAGGCCGCTCTACGGAATGGATTTACGTTACTGCCTGCATAAGGAGACCGGTGTAGCCAAGGACGAAGGCGACCCTAGGTTCTAACCGTCGACTTCGGCGGTAAGGTATCACTCAGGAAGCAGACACTGATAGACACGGTCTAGCAGATCGTTTGACGACTAGGTCAAATTGAGTGGTTTAATATCGGCATGTCTGGCTTTAGAATTCAGTATAGTGCGCTGATCCGAGTCGAATTAAAAACACCAGTACCCAAAACCAGGCGGGCTCGCCACGTCGGCTAATCCTGGTACATTTTGTAAACAATGTTCTGAAGAAAATTTGTGAAAGAAGGACGGGTCATCGCCTACTAATAGCAACAACGATCGGCCGCACCTTCCATTGTCGTGGCGACGCTCGGATTACACGGCAAAGGTGCTTGTGTTCCGACAGGCTAGCATATAATCCTGAGGCGTTACCCCAATCGTTCACCGTCGGATTTGCTACAGCCCCTGAACGCTACATGTACGAAACCATGTTATGTATGCACTAGGCCAACAATAGGACGTAGCCTTGTAGTTAGTACGTAGCCTGGTCGCATAAGTACAGTAGATCCTCCCCGCGCATCCTATTTATTAAGTTAATTCTACAGCAAAACGATCATATGCAGATCCGCAGTGGCCGGTAGACACACGTCCACCCCGCTGCTCTGTGACAGGGACTAAAGAGGCGAAGATTATCGTGTGTGCCCCGTTATGGTCGAGTTCGGTCAGAGCGTCATTGCGAGTAGTCGTTTGCTTTCTCGAATTCCGAGCGATTAAGCGTGACAGTCCCAGCGAACCCACAAAACGTGATCGCAGTCCATGCGATCATACGCAAGAAGGAAGGTCCCCATACACCGACGCACCAGTTTACACGCCGTATGCATAAACGAGCTGCACAAACGAGAGTGCTTGAACTGGACCTCTAGTTCCTCTACAAAGAACAGGTTGACCTGTCGCGAAGTTGCCTTGCCTAGATGCAATGTCGGACGTATTACTTTTGCCTCAACGGCTCCTGCTTTCGCTGAAACCCAAGACAGGCAACAGTAACCGCCTTTTGAAGGCGAGTCCTTCGTCTGTGACTAACTGTGCCAAATCGTCTTCCAAACTCCTAATCCAGTTTAACTCACCAAATTATAGCCATACAGACCCTAATTTCATATCATATCACGCCATTAGCCTCTGCTAAAATTCTGTGCTCAAGGGTTTTGGTTCGCCCGAGTGATGTTGCCAATTAGGACCATCAAATGCACATGTTACAGGACTTCTTATAAATACTTTTTTCCTGGGGAGTAGCGGATCTTAATGGATGTTGCCAGCTGGTATGGAAGCTAATAGCGCCGGTGGGAGCGTAATCTGCCGTCTCCACCAACACAACGCTATCGGGTCATATTATAAGATTCCGCAATGGGGTTACTTATAGGTAGCCTTAACGATATCCGGAACTTGCGATGTACGTGCTATGCTTTAATACATACCTGGCCCAGTAGTTTTCCAATATGGGAACATCAATTGTACATCGGGCCGGGATAATCATGTCATCACGGAAGTAGCCGTAAGACAAATAATTCAAAAGAGATGTCGTTTTGCTAGTTCACGTGAAGGTGTCTCGCGCCACCTCTAAGTAAGTGGGCCGTCGAGACATTATCCCTGATTTTTTCACTACTATTAGTACTCACGGCGCAATACCACCACAGCCTTGTCTCGCCAGAATGCTGGTCAGCATACGGAAGAGCTCAAGGCAGGTC" into "Template" input
    And user entered "100" into "Forward primer from" input
    And user entered "1000" into "Forward primer to" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then page title should become "Primer-Blast results"
    
  Scenario: Req1_User can start primers design and specify reverse primer parameters
    Given application URL is opened
    And user entered "GTCAAGCCAGTCACGCAGTAACGTTCATCAGCTAACGTAACAGTTAGAGGCTCGCTAAATCGCACTGTCGGCGTCCCTTGGGTATTTTACGCTAGCATCAGGTAGGCTAGCATGTATCTTTCCTCCCAGGGGTATGCGGGTGCGTGGACAAATGAGCAGCAAACGTAAATTCTCGGCGTGCTTGGTGTCTCGTATTTCTCCTGGAGATCAAGGAAATGTTTCATGACCAAGCGAAAGGCCGCTCTACGGAATGGATTTACGTTACTGCCTGCATAAGGAGACCGGTGTAGCCAAGGACGAAGGCGACCCTAGGTTCTAACCGTCGACTTCGGCGGTAAGGTATCACTCAGGAAGCAGACACTGATAGACACGGTCTAGCAGATCGTTTGACGACTAGGTCAAATTGAGTGGTTTAATATCGGCATGTCTGGCTTTAGAATTCAGTATAGTGCGCTGATCCGAGTCGAATTAAAAACACCAGTACCCAAAACCAGGCGGGCTCGCCACGTCGGCTAATCCTGGTACATTTTGTAAACAATGTTCTGAAGAAAATTTGTGAAAGAAGGACGGGTCATCGCCTACTAATAGCAACAACGATCGGCCGCACCTTCCATTGTCGTGGCGACGCTCGGATTACACGGCAAAGGTGCTTGTGTTCCGACAGGCTAGCATATAATCCTGAGGCGTTACCCCAATCGTTCACCGTCGGATTTGCTACAGCCCCTGAACGCTACATGTACGAAACCATGTTATGTATGCACTAGGCCAACAATAGGACGTAGCCTTGTAGTTAGTACGTAGCCTGGTCGCATAAGTACAGTAGATCCTCCCCGCGCATCCTATTTATTAAGTTAATTCTACAGCAAAACGATCATATGCAGATCCGCAGTGGCCGGTAGACACACGTCCACCCCGCTGCTCTGTGACAGGGACTAAAGAGGCGAAGATTATCGTGTGTGCCCCGTTATGGTCGAGTTCGGTCAGAGCGTCATTGCGAGTAGTCGTTTGCTTTCTCGAATTCCGAGCGATTAAGCGTGACAGTCCCAGCGAACCCACAAAACGTGATCGCAGTCCATGCGATCATACGCAAGAAGGAAGGTCCCCATACACCGACGCACCAGTTTACACGCCGTATGCATAAACGAGCTGCACAAACGAGAGTGCTTGAACTGGACCTCTAGTTCCTCTACAAAGAACAGGTTGACCTGTCGCGAAGTTGCCTTGCCTAGATGCAATGTCGGACGTATTACTTTTGCCTCAACGGCTCCTGCTTTCGCTGAAACCCAAGACAGGCAACAGTAACCGCCTTTTGAAGGCGAGTCCTTCGTCTGTGACTAACTGTGCCAAATCGTCTTCCAAACTCCTAATCCAGTTTAACTCACCAAATTATAGCCATACAGACCCTAATTTCATATCATATCACGCCATTAGCCTCTGCTAAAATTCTGTGCTCAAGGGTTTTGGTTCGCCCGAGTGATGTTGCCAATTAGGACCATCAAATGCACATGTTACAGGACTTCTTATAAATACTTTTTTCCTGGGGAGTAGCGGATCTTAATGGATGTTGCCAGCTGGTATGGAAGCTAATAGCGCCGGTGGGAGCGTAATCTGCCGTCTCCACCAACACAACGCTATCGGGTCATATTATAAGATTCCGCAATGGGGTTACTTATAGGTAGCCTTAACGATATCCGGAACTTGCGATGTACGTGCTATGCTTTAATACATACCTGGCCCAGTAGTTTTCCAATATGGGAACATCAATTGTACATCGGGCCGGGATAATCATGTCATCACGGAAGTAGCCGTAAGACAAATAATTCAAAAGAGATGTCGTTTTGCTAGTTCACGTGAAGGTGTCTCGCGCCACCTCTAAGTAAGTGGGCCGTCGAGACATTATCCCTGATTTTTTCACTACTATTAGTACTCACGGCGCAATACCACCACAGCCTTGTCTCGCCAGAATGCTGGTCAGCATACGGAAGAGCTCAAGGCAGGTC" into "Template" input
    And user entered "1200" into "Reverse primer from" input
    And user entered "1700" into "Reverse primer to" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then page title should become "Primer-Blast results"
    
  Scenario: Req1_User can start primers design and return 20 primers instead of default 10
    Given application URL is opened
    And user entered "GTCAAGCCAGTCACGCAGTAACGTTCATCAGCTAACGTAACAGTTAGAGGCTCGCTAAATCGCACTGTCGGCGTCCCTTGGGTATTTTACGCTAGCATCAGGTAGGCTAGCATGTATCTTTCCTCCCAGGGGTATGCGGGTGCGTGGACAAATGAGCAGCAAACGTAAATTCTCGGCGTGCTTGGTGTCTCGTATTTCTCCTGGAGATCAAGGAAATGTTTCATGACCAAGCGAAAGGCCGCTCTACGGAATGGATTTACGTTACTGCCTGCATAAGGAGACCGGTGTAGCCAAGGACGAAGGCGACCCTAGGTTCTAACCGTCGACTTCGGCGGTAAGGTATCACTCAGGAAGCAGACACTGATAGACACGGTCTAGCAGATCGTTTGACGACTAGGTCAAATTGAGTGGTTTAATATCGGCATGTCTGGCTTTAGAATTCAGTATAGTGCGCTGATCCGAGTCGAATTAAAAACACCAGTACCCAAAACCAGGCGGGCTCGCCACGTCGGCTAATCCTGGTACATTTTGTAAACAATGTTCTGAAGAAAATTTGTGAAAGAAGGACGGGTCATCGCCTACTAATAGCAACAACGATCGGCCGCACCTTCCATTGTCGTGGCGACGCTCGGATTACACGGCAAAGGTGCTTGTGTTCCGACAGGCTAGCATATAATCCTGAGGCGTTACCCCAATCGTTCACCGTCGGATTTGCTACAGCCCCTGAACGCTACATGTACGAAACCATGTTATGTATGCACTAGGCCAACAATAGGACGTAGCCTTGTAGTTAGTACGTAGCCTGGTCGCATAAGTACAGTAGATCCTCCCCGCGCATCCTATTTATTAAGTTAATTCTACAGCAAAACGATCATATGCAGATCCGCAGTGGCCGGTAGACACACGTCCACCCCGCTGCTCTGTGACAGGGACTAAAGAGGCGAAGATTATCGTGTGTGCCCCGTTATGGTCGAGTTCGGTCAGAGCGTCATTGCGAGTAGTCGTTTGCTTTCTCGAATTCCGAGCGATTAAGCGTGACAGTCCCAGCGAACCCACAAAACGTGATCGCAGTCCATGCGATCATACGCAAGAAGGAAGGTCCCCATACACCGACGCACCAGTTTACACGCCGTATGCATAAACGAGCTGCACAAACGAGAGTGCTTGAACTGGACCTCTAGTTCCTCTACAAAGAACAGGTTGACCTGTCGCGAAGTTGCCTTGCCTAGATGCAATGTCGGACGTATTACTTTTGCCTCAACGGCTCCTGCTTTCGCTGAAACCCAAGACAGGCAACAGTAACCGCCTTTTGAAGGCGAGTCCTTCGTCTGTGACTAACTGTGCCAAATCGTCTTCCAAACTCCTAATCCAGTTTAACTCACCAAATTATAGCCATACAGACCCTAATTTCATATCATATCACGCCATTAGCCTCTGCTAAAATTCTGTGCTCAAGGGTTTTGGTTCGCCCGAGTGATGTTGCCAATTAGGACCATCAAATGCACATGTTACAGGACTTCTTATAAATACTTTTTTCCTGGGGAGTAGCGGATCTTAATGGATGTTGCCAGCTGGTATGGAAGCTAATAGCGCCGGTGGGAGCGTAATCTGCCGTCTCCACCAACACAACGCTATCGGGTCATATTATAAGATTCCGCAATGGGGTTACTTATAGGTAGCCTTAACGATATCCGGAACTTGCGATGTACGTGCTATGCTTTAATACATACCTGGCCCAGTAGTTTTCCAATATGGGAACATCAATTGTACATCGGGCCGGGATAATCATGTCATCACGGAAGTAGCCGTAAGACAAATAATTCAAAAGAGATGTCGTTTTGCTAGTTCACGTGAAGGTGTCTCGCGCCACCTCTAAGTAAGTGGGCCGTCGAGACATTATCCCTGATTTTTTCACTACTATTAGTACTCACGGCGCAATACCACCACAGCCTTGTCTCGCCAGAATGCTGGTCAGCATACGGAAGAGCTCAAGGCAGGTC" into "Template" input
    And user entered "20" into "Number of primers to return" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then page title should become "Primer-Blast results"
    
   Scenario: Req2_User can see primer pairs and attributes, specify Minimal size of PCR product
    Given application URL is opened
    And user entered "GTCAAGCCAGTCACGCAGTAACGTTCATCAGCTAACGTAACAGTTAGAGGCTCGCTAAATCGCACTGTCGGCGTCCCTTGGGTATTTTACGCTAGCATCAGGTAGGCTAGCATGTATCTTTCCTCCCAGGGGTATGCGGGTGCGTGGACAAATGAGCAGCAAACGTAAATTCTCGGCGTGCTTGGTGTCTCGTATTTCTCCTGGAGATCAAGGAAATGTTTCATGACCAAGCGAAAGGCCGCTCTACGGAATGGATTTACGTTACTGCCTGCATAAGGAGACCGGTGTAGCCAAGGACGAAGGCGACCCTAGGTTCTAACCGTCGACTTCGGCGGTAAGGTATCACTCAGGAAGCAGACACTGATAGACACGGTCTAGCAGATCGTTTGACGACTAGGTCAAATTGAGTGGTTTAATATCGGCATGTCTGGCTTTAGAATTCAGTATAGTGCGCTGATCCGAGTCGAATTAAAAACACCAGTACCCAAAACCAGGCGGGCTCGCCACGTCGGCTAATCCTGGTACATTTTGTAAACAATGTTCTGAAGAAAATTTGTGAAAGAAGGACGGGTCATCGCCTACTAATAGCAACAACGATCGGCCGCACCTTCCATTGTCGTGGCGACGCTCGGATTACACGGCAAAGGTGCTTGTGTTCCGACAGGCTAGCATATAATCCTGAGGCGTTACCCCAATCGTTCACCGTCGGATTTGCTACAGCCCCTGAACGCTACATGTACGAAACCATGTTATGTATGCACTAGGCCAACAATAGGACGTAGCCTTGTAGTTAGTACGTAGCCTGGTCGCATAAGTACAGTAGATCCTCCCCGCGCATCCTATTTATTAAGTTAATTCTACAGCAAAACGATCATATGCAGATCCGCAGTGGCCGGTAGACACACGTCCACCCCGCTGCTCTGTGACAGGGACTAAAGAGGCGAAGATTATCGTGTGTGCCCCGTTATGGTCGAGTTCGGTCAGAGCGTCATTGCGAGTAGTCGTTTGCTTTCTCGAATTCCGAGCGATTAAGCGTGACAGTCCCAGCGAACCCACAAAACGTGATCGCAGTCCATGCGATCATACGCAAGAAGGAAGGTCCCCATACACCGACGCACCAGTTTACACGCCGTATGCATAAACGAGCTGCACAAACGAGAGTGCTTGAACTGGACCTCTAGTTCCTCTACAAAGAACAGGTTGACCTGTCGCGAAGTTGCCTTGCCTAGATGCAATGTCGGACGTATTACTTTTGCCTCAACGGCTCCTGCTTTCGCTGAAACCCAAGACAGGCAACAGTAACCGCCTTTTGAAGGCGAGTCCTTCGTCTGTGACTAACTGTGCCAAATCGTCTTCCAAACTCCTAATCCAGTTTAACTCACCAAATTATAGCCATACAGACCCTAATTTCATATCATATCACGCCATTAGCCTCTGCTAAAATTCTGTGCTCAAGGGTTTTGGTTCGCCCGAGTGATGTTGCCAATTAGGACCATCAAATGCACATGTTACAGGACTTCTTATAAATACTTTTTTCCTGGGGAGTAGCGGATCTTAATGGATGTTGCCAGCTGGTATGGAAGCTAATAGCGCCGGTGGGAGCGTAATCTGCCGTCTCCACCAACACAACGCTATCGGGTCATATTATAAGATTCCGCAATGGGGTTACTTATAGGTAGCCTTAACGATATCCGGAACTTGCGATGTACGTGCTATGCTTTAATACATACCTGGCCCAGTAGTTTTCCAATATGGGAACATCAATTGTACATCGGGCCGGGATAATCATGTCATCACGGAAGTAGCCGTAAGACAAATAATTCAAAAGAGATGTCGTTTTGCTAGTTCACGTGAAGGTGTCTCGCGCCACCTCTAAGTAAGTGGGCCGTCGAGACATTATCCCTGATTTTTTCACTACTATTAGTACTCACGGCGCAATACCACCACAGCCTTGTCTCGCCAGAATGCTGGTCAGCATACGGAAGAGCTCAAGGCAGGTC" into "Template" input
    And user entered "50" into "Minimal size of PCR product" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then "Primers design" table should become visible
    
   Scenario: Req2_User can see primer pairs and attributes
    Given application URL is opened
    And user entered "GTCAAGCCAGTCACGCAGTAACGTTCATCAGCTAACGTAACAGTTAGAGGCTCGCTAAATCGCACTGTCGGCGTCCCTTGGGTATTTTACGCTAGCATCAGGTAGGCTAGCATGTATCTTTCCTCCCAGGGGTATGCGGGTGCGTGGACAAATGAGCAGCAAACGTAAATTCTCGGCGTGCTTGGTGTCTCGTATTTCTCCTGGAGATCAAGGAAATGTTTCATGACCAAGCGAAAGGCCGCTCTACGGAATGGATTTACGTTACTGCCTGCATAAGGAGACCGGTGTAGCCAAGGACGAAGGCGACCCTAGGTTCTAACCGTCGACTTCGGCGGTAAGGTATCACTCAGGAAGCAGACACTGATAGACACGGTCTAGCAGATCGTTTGACGACTAGGTCAAATTGAGTGGTTTAATATCGGCATGTCTGGCTTTAGAATTCAGTATAGTGCGCTGATCCGAGTCGAATTAAAAACACCAGTACCCAAAACCAGGCGGGCTCGCCACGTCGGCTAATCCTGGTACATTTTGTAAACAATGTTCTGAAGAAAATTTGTGAAAGAAGGACGGGTCATCGCCTACTAATAGCAACAACGATCGGCCGCACCTTCCATTGTCGTGGCGACGCTCGGATTACACGGCAAAGGTGCTTGTGTTCCGACAGGCTAGCATATAATCCTGAGGCGTTACCCCAATCGTTCACCGTCGGATTTGCTACAGCCCCTGAACGCTACATGTACGAAACCATGTTATGTATGCACTAGGCCAACAATAGGACGTAGCCTTGTAGTTAGTACGTAGCCTGGTCGCATAAGTACAGTAGATCCTCCCCGCGCATCCTATTTATTAAGTTAATTCTACAGCAAAACGATCATATGCAGATCCGCAGTGGCCGGTAGACACACGTCCACCCCGCTGCTCTGTGACAGGGACTAAAGAGGCGAAGATTATCGTGTGTGCCCCGTTATGGTCGAGTTCGGTCAGAGCGTCATTGCGAGTAGTCGTTTGCTTTCTCGAATTCCGAGCGATTAAGCGTGACAGTCCCAGCGAACCCACAAAACGTGATCGCAGTCCATGCGATCATACGCAAGAAGGAAGGTCCCCATACACCGACGCACCAGTTTACACGCCGTATGCATAAACGAGCTGCACAAACGAGAGTGCTTGAACTGGACCTCTAGTTCCTCTACAAAGAACAGGTTGACCTGTCGCGAAGTTGCCTTGCCTAGATGCAATGTCGGACGTATTACTTTTGCCTCAACGGCTCCTGCTTTCGCTGAAACCCAAGACAGGCAACAGTAACCGCCTTTTGAAGGCGAGTCCTTCGTCTGTGACTAACTGTGCCAAATCGTCTTCCAAACTCCTAATCCAGTTTAACTCACCAAATTATAGCCATACAGACCCTAATTTCATATCATATCACGCCATTAGCCTCTGCTAAAATTCTGTGCTCAAGGGTTTTGGTTCGCCCGAGTGATGTTGCCAATTAGGACCATCAAATGCACATGTTACAGGACTTCTTATAAATACTTTTTTCCTGGGGAGTAGCGGATCTTAATGGATGTTGCCAGCTGGTATGGAAGCTAATAGCGCCGGTGGGAGCGTAATCTGCCGTCTCCACCAACACAACGCTATCGGGTCATATTATAAGATTCCGCAATGGGGTTACTTATAGGTAGCCTTAACGATATCCGGAACTTGCGATGTACGTGCTATGCTTTAATACATACCTGGCCCAGTAGTTTTCCAATATGGGAACATCAATTGTACATCGGGCCGGGATAATCATGTCATCACGGAAGTAGCCGTAAGACAAATAATTCAAAAGAGATGTCGTTTTGCTAGTTCACGTGAAGGTGTCTCGCGCCACCTCTAAGTAAGTGGGCCGTCGAGACATTATCCCTGATTTTTTCACTACTATTAGTACTCACGGCGCAATACCACCACAGCCTTGTCTCGCCAGAATGCTGGTCAGCATACGGAAGAGCTCAAGGCAGGTC" into "Template" input
    And user entered "50" into "Minimal size of PCR product" input
     And user entered "500" into "Maximal size of PCR product" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then "Primers design" table should become visible
    
  Scenario: Req3_User provided incorrect settings
    Given application URL is opened
    And user entered "GTCAAGCCAGTCACGCAGTAACGTTCATCAGCTAACGTAACAGTTAGAGGCTCGCTAAATCGCACTGTCGGCGTCCCTTGGGTATTTTACGCTAGCATCAGGTAGGCTAGCATGTATCTTTCCTCCCAGGGGTATGCGGGTGCGTGGACAAATGAGCAGCAAACGTAAATTCTCGGCGTGCTTGGTGTCTCGTATTTCTCCTGGAGATCAAGGAAATGTTTCATGACCAAGCGAAAGGCCGCTCTACGGAATGGATTTACGTTACTGCCTGCATAAGGAGACCGGTGTAGCCAAGGACGAAGGCGACCCTAGGTTCTAACCGTCGACTTCGGCGGTAAGGTATCACTCAGGAAGCAGACACTGATAGACACGGTCTAGCAGATCGTTTGACGACTAGGTCAAATTGAGTGGTTTAATATCGGCATGTCTGGCTTTAGAATTCAGTATAGTGCGCTGATCCGAGTCGAATTAAAAACACCAGTACCCAAAACCAGGCGGGCTCGCCACGTCGGCTAATCCTGGTACATTTTGTAAACAATGTTCTGAAGAAAATTTGTGAAAGAAGGACGGGTCATCGCCTACTAATAGCAACAACGATCGGCCGCACCTTCCATTGTCGTGGCGACGCTCGGATTACACGGCAAAGGTGCTTGTGTTCCGACAGGCTAGCATATAATCCTGAGGCGTTACCCCAATCGTTCACCGTCGGATTTGCTACAGCCCCTGAACGCTACATGTACGAAACCATGTTATGTATGCACTAGGCCAACAATAGGACGTAGCCTTGTAGTTAGTACGTAGCCTGGTCGCATAAGTACAGTAGATCCTCCCCGCGCATCCTATTTATTAAGTTAATTCTACAGCAAAACGATCATATGCAGATCCGCAGTGGCCGGTAGACACACGTCCACCCCGCTGCTCTGTGACAGGGACTAAAGAGGCGAAGATTATCGTGTGTGCCCCGTTATGGTCGAGTTCGGTCAGAGCGTCATTGCGAGTAGTCGTTTGCTTTCTCGAATTCCGAGCGATTAAGCGTGACAGTCCCAGCGAACCCACAAAACGTGATCGCAGTCCATGCGATCATACGCAAGAAGGAAGGTCCCCATACACCGACGCACCAGTTTACACGCCGTATGCATAAACGAGCTGCACAAACGAGAGTGCTTGAACTGGACCTCTAGTTCCTCTACAAAGAACAGGTTGACCTGTCGCGAAGTTGCCTTGCCTAGATGCAATGTCGGACGTATTACTTTTGCCTCAACGGCTCCTGCTTTCGCTGAAACCCAAGACAGGCAACAGTAACCGCCTTTTGAAGGCGAGTCCTTCGTCTGTGACTAACTGTGCCAAATCGTCTTCCAAACTCCTAATCCAGTTTAACTCACCAAATTATAGCCATACAGACCCTAATTTCATATCATATCACGCCATTAGCCTCTGCTAAAATTCTGTGCTCAAGGGTTTTGGTTCGCCCGAGTGATGTTGCCAATTAGGACCATCAAATGCACATGTTACAGGACTTCTTATAAATACTTTTTTCCTGGGGAGTAGCGGATCTTAATGGATGTTGCCAGCTGGTATGGAAGCTAATAGCGCCGGTGGGAGCGTAATCTGCCGTCTCCACCAACACAACGCTATCGGGTCATATTATAAGATTCCGCAATGGGGTTACTTATAGGTAGCCTTAACGATATCCGGAACTTGCGATGTACGTGCTATGCTTTAATACATACCTGGCCCAGTAGTTTTCCAATATGGGAACATCAATTGTACATCGGGCCGGGATAATCATGTCATCACGGAAGTAGCCGTAAGACAAATAATTCAAAAGAGATGTCGTTTTGCTAGTTCACGTGAAGGTGTCTCGCGCCACCTCTAAGTAAGTGGGCCGTCGAGACATTATCCCTGATTTTTTCACTACTATTAGTACTCACGGCGCAATACCACCACAGCCTTGTCTCGCCAGAATGCTGGTCAGCATACGGAAGAGCTCAAGGCAGGTC" into "Template" input
    And user entered "500" into "Forward primer from" input
    And user entered "1000" into "Forward primer to" input
    And user entered "700" into "Reverse primer from" input
    And user entered "1500" into "Reverse primer to" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then label "Error" should become visible
    
  Scenario: Req3_User provided ivalid input - not nucleotides
    Given application URL is opened
    And user entered "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV" into "Template" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then label "Error" should become visible
    
  Scenario: Req3_User provided ivalid input - just space
    Given application URL is opened
    And user entered " " into "Template" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then label "Error" should become visible
    
  Scenario: Req4_Primer evaluation, user provides accession number
    Given application URL is opened
    And user entered "NZ_CP130713.1" into "Template" input
    And user entered "100" into "Forward primer from" input
    And user entered "1000" into "Forward primer to" input
    And user entered "1200" into "Reverse primer from" input
    And user entered "1900" into "Reverse primer to" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then "Primers design" table should become visible

  Scenario: Req4_Primer evaluation, user provides primers
    Given application URL is opened
    And user entered "AAGGGAACGCTGTCTGAAGG" into "Forward primer" input
    And user entered "AGCTGATATGTCGCGTCAGG" into "Reverse primer" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then "Primers design" table should become visible
    
  Scenario: Req4_Primer evaluation, provide accession number (DNA) and user provides primers
    Given application URL is opened
    And user entered "NZ_CP130713.1" into "Template" input
    And user entered "100" into "Forward primer from" input
    And user entered "1000" into "Forward primer to" input
    And user entered "1200" into "Reverse primer from" input
    And user entered "1900" into "Reverse primer to" input
    And user entered "GATCTCCTCGAGCTTGTCGG" into "Forward primer" input
    And user entered "GACTGGCGACAGGTCTTCAA" into "Reverse primer" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then "Primers design" table should become visible
   
  Scenario: Req5_Primer evaluation, provide accession number (RNA) and specifies min max overlaps
    Given application URL is opened
    And user entered "NR_046235.3" into "Template" input
    And user entered "8" into "Min site overlap by five prime end" input
    And user entered "5" into "Min site overlap by three prime end" input
    And user entered "9" into "Max site overlap by three prime end" input
    And "Perform specificity check" checkbox is unchecked
    When user clicks on "Get primers" button
    Then "Primers design" table should become visible
  