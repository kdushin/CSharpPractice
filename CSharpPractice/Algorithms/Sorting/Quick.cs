namespace CSharpPractice.Algorithms.Sorting
{
    public static class Quick
    {
        public static void Sort(int[] arrayToSort)
        {
            var startIndex = 0;
            var endIndex = arrayToSort.Length - 1;

            QuickSortRecursive(arrayToSort, startIndex, endIndex);
        }

        private static void QuickSortRecursive(int[] arrayToSort, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                var pivotIndex = Partition(arrayToSort, startIndex, endIndex);
                QuickSortRecursive(arrayToSort, startIndex, pivotIndex - 1);
                QuickSortRecursive(arrayToSort, pivotIndex + 1, endIndex);
            }
        }

        private static int Partition(int[] arrayToSort, int startIndex, int endIndex)
        {
            var pivotIndex = endIndex;
            var pivotValue = arrayToSort[pivotIndex];
            var smallerPartitionEndIndex = startIndex - 1;
            for (int currentPosition = startIndex; currentPosition < pivotIndex; currentPosition++)
            {
                if (arrayToSort[currentPosition] <= pivotValue)
                {
                    SwapItemsInArray(arrayToSort, ++smallerPartitionEndIndex, currentPosition);
                }
            }

            pivotIndex = smallerPartitionEndIndex + 1;
            SwapItemsInArray(arrayToSort, pivotIndex, endIndex);

            return pivotIndex;
        }
        
        private static void SwapItemsInArray(int[] array, int firstIndex, int secondIndex)
        {
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }
    }
}