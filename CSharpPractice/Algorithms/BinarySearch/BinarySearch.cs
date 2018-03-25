namespace CSharpPractice.Algorithms.BinarySearch
{
    public class BinarySearch
    {
        /// <summary>
        /// Searches for "target" location in "numbers". If "numbers" doesn't contain "target" returns -1
        /// </summary>
        /// <param name="numbers">array of integers</param>
        /// <param name="target">target number that should be found in array</param>
        /// <returns>target location in specified array</returns>
        public static int FindItem(int[] numbers, int target)
        {
            var min = 0;
            var max = numbers.Length - 1;

            while (min <= max)
            {
                int guess = (min + max) / 2;

                if (numbers[guess] < target) min = guess + 1;

                else if (numbers[guess] > target) max = guess - 1;

                else return guess;
            }
            return -1;
        }
    }
}
