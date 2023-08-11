Feature: Second requirement (User can start design and see primers pairs and attributes for designed primers)
wiki: https://de.wikipedia.org/wiki/Primerdesign
DNA sequencing is used to read DNA (A, C, T, G nucleotides)
PCR is used to amplify little part of DNA (for example, 1000 bps)

 Background:
	Given application URL is opened

 @second_requirement
 Scenario: User can provide DNA template and specify setting for findings primers
	Given user entered "CGAATAGGGTATAATGCTTACAAGACTAGCCTTGCTAGCAACCGCGGGCTGGGGGGCTAAGGTATCACTCAAGAAGCAGGCTCGGTAACATACGGTCTAGCTATCGTGCGACCTAGGATAAGGTCGCCCTACAAAATAGATTTGTGCTACTCTCCTCATAAGCAGTCCGGTGTATCGAAAGAACAAGACTAGCCTTGCTAGCAACCGCGGGCTGGGGGGCTAAGGTATCACTCAAGAAGCAGGCTCGGTAACATACGGTCTAGCTATCTGACTATCGCCTACGTCATATAGGGACCTATGATATCTGCGTGTCCAACCTTAGAATTCACTTCAGCGCGCAGGTTTGGGTCGAGATAAAATCACCAGTACCCAAGACCACGGGCGCTCGGCGCCTTGGCTAATCCCGGTACATGTTGTTATAAATAATCAGTAGAAAATCTGTGTTAGAGGGTCGAGTCACCATATATCAAGAACGATATTAATCGGTGGGAGTATACTATCGCCTAACGTCATATAGGAGACCTATGATATCTGCGTGTCACAACCTTAGAATTCACTTCAGCGCGCAGGTTTGGGTCGAGATAAAATCACCAGTACCCAAGACCACGGGCGCTCGGCGCCTTGGCTAACTCCCGGTACATGTTGTTATAAATAATCAGTAGAAAATCTGTGTTAGAGGGTCGAGTCACCATATATCAAGAACGATATTAATCGGTGGGAGTATTCAACGTGATGAAGACGCTGGTGTTTACGTGGGAATGGTGCCAGTAGATCCTGCCCGCGTTTCCTATATATTAAGTTAAATCTTATGGAATATAATAACATGTTTCACGACCCTAGCTATCTGACTATCGCCTACGTCATATAGGGACCTATGATAGTCTGCGTGTCCAACCTTAGAGATTCCACTTCAGCGCGCAGGTTTGGGTCGAGATAAAATCACCAGTACCCAAGACCCACGGGCGTAGGATAAGGTCGCCCTACAAAATAGATTTGTGCTACTCTCCTCATAAGCAGTCCGGTGTATCGAAAGAACAAGACTAGGCCTTGCTAGCAACCTGCGGGCTGGGGGGCTA" into "Template" input
	When user enters "10" into "Forward primer from" input
	And user enters "430" into "Forward primer to" input
	And user enters "14" into "Number of primers to return" input
	And user enters "5" into "Min Site overlap by three prime end" input
	And user clicks on "Perform specificity check" checkbox
	And user clicks on "Get primers" button
	Then "Primers design" table should become visible

 Scenario: User can provide DNA template as GenBank number and specify setting for findings primers
	Given user entered "T78497" into "Template" input
	When user enters "10" into "Forward primer from" input
	And user enters "130" into "Forward primer to" input
	And user enters "5" into "Min Site overlap by three prime end" input
	And user clicks on "Perform specificity check" checkbox
	And user clicks on "Get primers" button
	Then "Primers design" table should become visible
