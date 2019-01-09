using System;

namespace CSharpPractice.Algorithms.Sorting
{
    public static class Quick
    {
        private static Random _randomizer = new Random();
        
        public static void Sort(int[] arrayToSort)
        {
            var startIndex = 0;
            var endIndex = arrayToSort.Length - 1;

            QuickSortInternal(arrayToSort, startIndex, endIndex);
        }

        private static void QuickSortInternal(int[] arrayToSort, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                var pivotIndex = Partition(arrayToSort, startIndex, endIndex);
                QuickSortInternal(arrayToSort, startIndex, pivotIndex - 1);
                QuickSortInternal(arrayToSort, pivotIndex + 1, endIndex);
            }
        }

        /// <summary>
        /// Chooses random pivot point and rearrange array[i...j] in the following parts:
        /// 1) items less than a pivot
        /// 2) pivot
        /// 3) items greater than a pivot 
        /// </summary>
        /// <returns></returns>
        public static int Partition(int[] arrayToSort, int i, int j)
        {
            int pivotIndex = ChoosePivot(startIndex: i, endIndex: j);
            SwapItemsInArray(arrayToSort, pivotIndex, i);
            pivotIndex = i;
            
            int partitionLessThanPivotEndIndex = i + 1;
            for (int currentPosition = i + 1; currentPosition <= j; currentPosition++)
            {
                if (arrayToSort[currentPosition] <= arrayToSort[pivotIndex])
                {
                    SwapItemsInArray(arrayToSort, partitionLessThanPivotEndIndex++, currentPosition);
                }
            }

            int sortedPivotIndex = partitionLessThanPivotEndIndex - 1;
            SwapItemsInArray(arrayToSort, pivotIndex, sortedPivotIndex);
            
            return sortedPivotIndex;
        }
        
        private static void SwapItemsInArray(int[] array, int firstIndex, int secondIndex)
        {
            var temp = array[firstIndex];
            array[firstIndex] = array[secondIndex];
            array[secondIndex] = temp;
        }

        private static int ChoosePivot(int startIndex, int endIndex)
        {
            return _randomizer.Next(startIndex, endIndex);
        }
    }
}