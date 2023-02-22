Feature: Majority vote result

I want to calculate the result of the vote and find the winner 
@mytag

Scenario: find the winner when the candidate have more than 50%
	Given the vote 
	 | candidate   | pourcentage |
	 | candidate1  |    0.6      |
	 | candidate2  |    0.1      |
	 | candidate3  |    0.3      |
	Then the candidate1 is the winner


Scenario: find the winner when no one have more than 50%
	Given the vote 
	 | candidate   | pourcentage |
	 | candidate1  |    0.4      |
	 | candidate2  |    0.1      |
	 | candidate3  |    0.3      |
	Then the candidate1 and the candidate3 advance the second round   



Scenario: find the winner for the second round
Given the vote 
	 | candidate   | pourcentage |
	 | candidate1  |    0.6     |
	 | candidate3  |    0.4      |
	And the candidate1 and the candidate3 advance the second round
	
	Then the candidate1 is the winner    

Scenario: find the winner for the second round where candidates have 50%
 Given the vote 
	 | candidate   | pourcentage |
	 | candidate1  |    0.5      |
	 | candidate2  |    0.5      |
Then declare no winner 
