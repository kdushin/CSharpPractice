using System;

namespace CSharpPractice.Algorithms.Sorting
{
    public static class Merge
    {
        /// <summary>
        /// Sorting given arr. Selection sort implementation
        /// Complexity: O(n log(n))
        /// </summary>
        /// <param name="arr">unsorted arr should contain at least 1 item</param>
        public static void Sort(int[] arr)
        {
            if (arr == null || arr.Length <= 0) throw new ArgumentOutOfRangeException();
            DivideAndMergeSort(arr, 0, arr.Length - 1);
        }

        /// <summary>
        /// Divide into sub arrays => sort => merge sort
        /// </summary>
        /// <param name="arr">Array[left...right]</param>
        /// <param name="left">left index</param>
        /// <param name="right">right index</param>
        private static void DivideAndMergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                // Divide and merge into sub arrays
                DivideAndMergeSort(arr, left, middle);
                DivideAndMergeSort(arr, middle + 1, right);

                // Merge sorted arrays
                MergeArrays(arr, left, middle, right);
            }
        }

        private static void MergeArrays(int[] arr, int left, int middle, int right)
        {
            // Create two temporary sub arrays
            var a = new int[middle - left + 1];
            var b = new int[right - middle];

            // Copy all data into sub arrays
            for (int index = 0; index < a.Length; index++) a[index] = arr[left + index];
            for (int index = 0; index < b.Length; index++) b[index] = arr[middle + 1 + index];

            // Sort and merge data from two subarrays into initial array
            int i = 0, j = 0, k = left;
            while (i < a.Length && j < b.Length)
            {
                if (a[i] <= b[j]) arr[k] = a[i++];
                else              arr[k] = b[j++];
                k++;
            }

            // Merge and sort remaining elements of subarrays into intital array
            while (i < a.Length) arr[k++] = a[i++];
            while (j < b.Length) arr[k++] = b[j++];
        }
    }
}
