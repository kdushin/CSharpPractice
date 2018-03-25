using System;

namespace CSharpPractice.Algorithms.Sorting
{
    public static class Insertion
    {
        /// <summary>
        /// Sorting given array. Selection sort implementation
        /// Complexity: O(n^2)
        /// </summary>
        /// <param name="array">unsorted array should contain at least 1 item</param>
        public static void Sort(int[] array)
        {
            if (array == null || array.Length == 0) throw new ArgumentNullException(nameof(array));
            for (int j = 1; j < array.Length; j++)
            {
                int value = array[j];
                int i = j;
                while (i > 0 && array[i - 1] >= value)
                {
                    array[i] = array[i - 1];
                    i--;
                }
                array[i] = value;
            }
        }
    }
}
