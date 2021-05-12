# QuChallenge

This is the solution to the problem sent via email. It's quite simple and straight forward, it generates a matrix (up to 64x64), and based on a word stream, it looks for the matching words inside the matrix. Words are considered as a match if they are found from left to right and up to down only.



# What we have done

1. A class (**WordFinder**) that has the constructor in which we specify the matrix lines
2. A function inside above class that does the Find operation
3. A console application that runs a simple example
4. A Unit Tests project in which we have checked
   1. Matrix creation validation
   2. Find operations in horizontal and vertical positions
   3. Performance tests regarding time consumed when wordstream is large