using System.Collections.Generic;

namespace CSharpPractice.Algorithms.Sorting
{
    public class NumberOfInversions
    {
        public static int Count(int[] arr)
        {
            return Count(arr, 0, arr.Length - 1);
        }

        private static int Count(int[] arr, int left, int right)
        {
            var count = 0;
            if (right > left)
            {
                int middle = (right - left) / 2 + left;
                count += Count(arr, left, middle); // count left inversions recuresively
                count += Count(arr, middle + 1, right); // count right inversions recuresively
                count += MergeAndCount(arr, left, middle + 1, right); // count split inversions
            }

            return count;
        }

        private static int MergeAndCount(int[] arr, int left, int middle, int right)
        {
            int invCount = 0;
            var tempList = new List<int>(capacity: right);
            int i = left, j = middle;
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

            int k = left;
            foreach (int item in tempList) arr[k++] = item;

            return invCount;
        }
    }
}
