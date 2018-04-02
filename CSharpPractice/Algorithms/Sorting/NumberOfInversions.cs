using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpPractice.Algorithms.Sorting
{
    public class NumberOfInversions
    {
        //{1, 5, 2, 6}
        public static int SortAndCountInversions(int[] a, int left, int right)
        {
            int numberOfInversions = 0;
            var tempArr = new int[a.Length];
            if (right > left)
            {
                int middle = (left + right) / 2;

                // Divide and merge into sub arrays
                numberOfInversions += SortAndCountInversions(a, left, middle);
                numberOfInversions += SortAndCountInversions(a, middle + 1, right);
                numberOfInversions += MergeAndCountSplitInversion(a, tempArr, left, middle + 1, right);
            }

            return numberOfInversions;
        }

        private static int MergeAndCountSplitInversion(int[] arr, int[] temp, int left, int middle, int right)
        {
            
        }
    }
}
