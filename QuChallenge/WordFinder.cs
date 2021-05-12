using System;
using System.Collections.Generic;
using System.Linq;

namespace QuChallenge
{
    public class WordFinder
    {
        public readonly List<string> combinations = new List<string>();

        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null || !matrix.Any() || matrix.Count() > 64)
                throw new ArgumentException("The length of the input strings is incorrect");

            if (matrix.Any(x => x.Length != matrix.Count()))
                throw new ArgumentException("The size of the input string is incorrect");

            //Add the horizontal combinations
            combinations.AddRange(matrix);

            //Concatenate characters to create vertical combinations
            for (var i = 0; i < matrix.Count(); i++)
            {
                var combination = new string(matrix.Select(c => c[i]).ToArray());

                combinations.Add(combination);
            }
        }

        /// <summary>
        /// The PDF indicates that this should return the top ten of repeated words and if a word is found more than once, count it as 1,
        /// which is confusing to determine what to return.
        /// </summary>
        /// <param name="wordstream"></param>
        /// <returns></returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            //Remove duplicates
            var nws = wordstream.Select(s => s.ToLower()).Distinct().ToList();

            //Create a dictionary with the word as key and occurrences as value
            var found = nws.ToDictionary(ws => ws, ws => 0);

            //Calculate number of occurrences in each possible combination
            foreach (var ws in nws)
            {
                foreach (var c in combinations)
                {
                    found[ws] += GetOccurrences(c, ws);
                }
            }

            //If none of the words in wordstream if found in the matrix return an empty list
            if (!found.Any(x => x.Value > 0))
                return new List<string>();

            //Get the top ten found words, sorted by number of occurrences
            var topTen = (from pair in found orderby pair.Value descending select pair.Key).Take(10);

            return topTen;



        }

        private static int GetOccurrences(string line, string s)
        {
            var count = 0;
            var position = 0;

            while ((position = line.IndexOf(s, position, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                position += s.Length;
                count++;
            }

            return count;
        }
    }
}
