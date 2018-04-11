namespace CSharpPractice.Algorithms.Sorting
{
    public static class Quick
    {
        public static void Sort(int[] arr)
        {
            if (arr.Length == 1) return;
            int p = ChoosePivot(arr, arr.Length);
            Partition(arr, 0, arr.Length);
            // recuresively sort 1st part
            // recuresively sort 2nd part
            
            void Partition(int[] a, int left, int right)
            {
                int pivotIndex = left;
                int i = left + 1;
                for (int j = left + 1; j < right; j++)
                {
                    if (a[j] < a[pivotIndex])
                    {
                        SwapItemsInArray(a, j, i);
                        i++;
                    }
                }
                SwapItemsInArray(a, pivotIndex, i - 1);
            }

            void SwapItemsInArray(int[] a, int leftIndex, int rightIndex)
            {
                int temp = a[leftIndex];
                a[leftIndex] = a[rightIndex];
                a[rightIndex] = temp;
            }
        }

        private static int ChoosePivot(int[] arr, int length)
        {
            return arr[0];
        }

        private static void Partition(int[] arr, int left, int right)
        {
            
        }
    }
}