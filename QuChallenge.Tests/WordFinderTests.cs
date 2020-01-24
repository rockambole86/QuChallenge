using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bogus;
using Xunit;

namespace QuChallenge.Tests
{
    public class WordFinderTests
    {
        private WordFinder _wordFinder = null;
        private readonly Faker _faker = new Faker();

        [Fact]
        public void Constructor_WhenNullMatrix_ReturnsError()
        {
            // Arrange
            List<string> matrix = null;

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Fact]
        public void Constructor_WhenEmptyMatrix_ReturnsError()
        {
            // Arrange
            var matrix = new List<string>();

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Fact]
        public void Constructor_WhenInvalidMatrixLength_ReturnsError()
        {
            // Arrange
            var matrix = new List<string>();

            var line = new string('X', 64);

            for (var i = 0; i < 50; i++)
            {
                matrix.Add(line);
            }

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Fact]
        public void Constructor_WhenInvalidMatrixSize_ReturnsError()
        {
            // Arrange
            var matrix = new List<string>();

            var line = new string('X', 50);

            for (var i = 0; i < 64; i++)
            {
                matrix.Add(line);
            }

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(matrix));
        }

        [Fact]
        public void Constructor_WhenValid_Returns()
        {
            // Arrange
            var matrix = new List<string>();

            var line = new string('X', 64);

            for (var i = 0; i < 64; i++)
            {
                matrix.Add(line);
            }

            // Act
            _wordFinder = new WordFinder(matrix);

            // Assert
            Assert.Equal(128, _wordFinder.combinations.Count);
            Assert.Equal(64, _wordFinder.combinations[0].Length);
        }

        [Fact]
        public void Find_WhenValid_ReturnsList()
        {
            // Arrange
            var words = _faker.Random.WordsArray(1000);
            var matrix = GenerateMatrix(words.Take(10));

            _wordFinder = new WordFinder(matrix);

            // Act
            var result = _wordFinder.Find(words);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(10, result.Count());
        }

        [Fact]
        public void Find_WhenNoWordsFound_ReturnsEmptyList()
        {
            // Arrange
            var words  = new List<string>{ "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve", "diez" };
            var matrix = GenerateMatrix(words);
            var wordstream  = _faker.Random.WordsArray(1000).ToList();

            wordstream.RemoveAll(x => x.Length < 6);

            _wordFinder = new WordFinder(matrix);

            // Act
            var result = _wordFinder.Find(wordstream);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Find_WhenVerticalValid_ReturnsList()
        {
            // Arrange
            var matrix = GenerateVerticalMatrix();
            var words = new List<string>{ "sample" };

            _wordFinder = new WordFinder(matrix);

            // Act
            var result = _wordFinder.Find(words);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());
        }


        // Simple matrix generator, no vertical words
        private List<string> GenerateMatrix(IEnumerable<string> words)
        {
            const int size = 64;

            var line = new string('X', size);

            var matrix = new List<string>();

            for (var i = 0; i < size; i++)
            {
                var randomWord = words.ToList()[_faker.Random.Int(0, 9)];

                line = line.Substring(0, line.Length - randomWord.Length);

                var randomPosition = _faker.Random.Int(0, line.Length - 1);

                line = line.Insert(randomPosition, randomWord);

                matrix.Add(line);
            }

            return matrix;
        }

        private List<string> GenerateVerticalMatrix()
        {
            const int size = 64;

            var line = new string('X', size);

            var matrix = new List<string>
            {
                $"s{new string('X', size - 1)}",
                $"a{new string('X', size - 1)}",
                $"m{new string('X', size - 1)}",
                $"p{new string('X', size - 1)}",
                $"l{new string('X', size - 1)}",
                $"e{new string('X', size - 1)}"
            };


            for (var i = 0; i < size - 6; i++)
            {
                matrix.Add(line);
            }

            return matrix;
        }
    }
}
