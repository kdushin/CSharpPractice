using System.Collections.Generic;

namespace CSharpPractice.Algorithms.Sorting
{
    public class NumberOfInversions
    {
        public static long Count(int[] arr)
        {
            return Count(arr, 0, arr.Length - 1);
        }

        private static long Count(int[] arr, long left, long right)
        {
            long count = 0L;
            if (right > left)
            {
                long middle = (right - left) / 2L + left;
                count += Count(arr, left, middle); // count left inversions recuresively
                count += Count(arr, middle + 1, right); // count right inversions recuresively
                count += MergeAndCount(arr, left, middle + 1, right); // count split inversions
            }

            return count;
        }

        private static long MergeAndCount(int[] arr, long left, long middle, long right)
        {
            long invCount = 0L;
            var tempList = new List<int>(capacity: (int)right);
            long i = left, j = middle;
            while (i < middle && j <= right)
            {
                if (arr[i] <= arr[j]) tempList.Add(arr[i++]);
                else
                {
                    tempList.Add(arr[j++]);
                    invCount = invCount + middle - i;
                }
            }

            while (i < middle) tempList.Add(arr[i++]);
            while (j <= right) tempList.Add(arr[j++]);

            long k = left;
            foreach (int item in tempList) arr[k++] = item;

            return invCount;
        }
    }
}
