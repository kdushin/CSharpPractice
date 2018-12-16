﻿using System;

namespace CSharpPractice.Algorithms.Sorting
{
    public static class Quick
    {
        private static Random _randomizer = new Random();
        
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

        private static int Partition(int[] arrayToSort, int i, int j)
        {
            var pivotIndex = ChoosePivot(startIndex: i, endIndex: j);
            var pivot = arrayToSort[pivotIndex];
            var smallerPartitionEndIndex = i - 1;
            for (int currentPosition = i; currentPosition < pivotIndex; currentPosition++)
            {
                if (arrayToSort[currentPosition] <= pivot)
                {
                    SwapItemsInArray(arrayToSort, ++smallerPartitionEndIndex, currentPosition);
                }
            }

            pivotIndex = smallerPartitionEndIndex + 1;
            SwapItemsInArray(arrayToSort, pivotIndex, j);

            return pivotIndex;
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