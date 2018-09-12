using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPractice.Algorithms.Sorting
{
    // TODO: Don't be lazy. Add tests for this!
    public class LinearSort
    {
        public int[] NaiveCountingSort(int[] array, int range)
        {
            var valuesCount = new int[range + 1];
            var output = new int[array.Length];
            var currentIndex = 0;

            foreach (var i in array)
            {
                valuesCount[i] += 1;
            }

            for (var i = 0; i <= range; i++)
            {
                var valueCount = valuesCount[i];
                while (valueCount != 0)
                {
                    output[currentIndex++] = i;
                    valueCount--;
                }    
            }

            return output;
        }

        public int[] CountingSortStable(int[] arrayToSort, int range)
        {
            var valuesCount = new int[range];
            var output = new int[arrayToSort.Length];

            foreach (var i in arrayToSort)
            {
                valuesCount[i] += 1;
            }

            for (int i = 1; i < range; i++)
            {
                valuesCount[i] += valuesCount[i - 1];
            }

            for (int i = arrayToSort.Length - 1; i >= 0; i--)
            {
                output[valuesCount[arrayToSort[i]] - 1] = arrayToSort[i];
                valuesCount[arrayToSort[i]]--;
            }

            return output;
        }

        public int[] RadixSort(int[] arrayToSort, int digitsCount)
        {
            for (var digitsProcessed = 0; digitsProcessed < digitsCount; digitsProcessed++)
            {
                var divisor = (int)Math.Pow(10, digitsProcessed);
                var singleDigitArray = arrayToSort.Select(x => x / divisor % 10).ToArray();
                arrayToSort = RadixSubroutineCountingSort(singleDigitArray, arrayToSort);
            }

            return arrayToSort;
        }

        public string[] RadixCharacterSort(string[] words)
        {
            var wordLength = words[0].Length;
            var wordNumberRepresentation = new int[words.Length];
            var numberToWord = new Dictionary<int, string>();

            for (var q = 0; q < words.Length; q++)
            {
                var currentWord = words[q];
                var numberRepresentation = 0;

                for (int i = 0, z = (wordLength - 1) * 2; i < wordLength; i++, z -= 2)
                {
                    var multiplier = (int)Math.Pow(10, z);
                    numberRepresentation += char.ToUpper(currentWord[i]) * multiplier;
                }
                numberToWord.Add(numberRepresentation, currentWord);
                wordNumberRepresentation[q] = numberRepresentation;
            }

            wordNumberRepresentation = RadixSort(wordNumberRepresentation, wordLength * 2);

            return wordNumberRepresentation.Select(x => numberToWord[x]).ToArray();
        }

        private int[] RadixSubroutineCountingSort(int[] singleDigitsArray, int[] originalArray, int range = 10)
        {
            var valuesCount = new int[range];
            var output = new int[singleDigitsArray.Length];

            foreach (var i in singleDigitsArray)
            {
                valuesCount[i] += 1;
            }

            for (int i = 1; i < range; i++)
            {
                valuesCount[i] += valuesCount[i - 1];
            }

            for (int i = singleDigitsArray.Length - 1; i >= 0; i--)
            {
                output[valuesCount[singleDigitsArray[i]] - 1] = originalArray[i];
                valuesCount[singleDigitsArray[i]]--;
            }

            return output;
        }
    }
}