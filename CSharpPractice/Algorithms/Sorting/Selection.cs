using System;

namespace CSharpPractice.Algorithms.Sorting
{
    public static class Selection
    {
        /// <summary>
        /// Sorting given array. Selection sort implementation
        /// Complexity: O(n^2)
        /// </summary>
        /// <param name="array">unsorted array should contain at least 1 item</param>
        public static void Sort(int[] array)
        {
            if (array == null || array.Length == 0) throw new ArgumentNullException(nameof(array));
            for (var index = 0; index < array.Length; index++)
            {  
                var minIndex = IndexOfMinimum(array, index);
                SwapItemsInArray(array, index, minIndex);
            }
        }

        private static void SwapItemsInArray(int[] array, int firstIndex, int secondIndex)
        {
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        private static int IndexOfMinimum(int[] array, int startIndex)
        {
            var minIndex = startIndex;
            var minValue = array[minIndex];

            for (var index = minIndex + 1; index < array.Length; index++)
            {
                if (array[index] < minValue)
                {
                    minIndex = index;
                    minValue = array[minIndex];
                }
            }
            return minIndex;
        }
    }
}
