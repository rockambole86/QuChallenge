# QuChallenge

This is the solution to the problem sent via email. It's quite simple and straight forward, it generates a matrix (64x64), and based on a word list, it looks for the matching words inside the matrix. Words are considered as a match if they are found from left to right and up to down only.

# Concerns

The document indicates that the **Find** function should return the top ten words with most occurrences, but also says that if a word is found more than once, it should be considered as one; which is contradictory. 
