using AlgorithmBasics.Algorithms.Sorting;

namespace AlgorithmBasics.Algorithms
{
    public static class OrderStatistics
    {
        /// <summary>
        /// Return ith smallest element of the array[p...r]
        /// </summary>
        public static int RSelect(int[] array, int p, int r, int i)
        {
            if (p == r)
            {
                return array[p];
            }

            int pivotIndex = Quick.Partition(array, p, r);
            int k = pivotIndex - p + 1;
            if (i <= k)
            {
                return RSelect(array, p, pivotIndex, i);
            }
            else
            {
                return RSelect(array, pivotIndex + 1, r, i - k);
            }
        }
    }
}