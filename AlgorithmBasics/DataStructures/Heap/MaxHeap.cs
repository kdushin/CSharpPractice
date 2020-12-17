
namespace AlgorithmBasics.DataStructures.Heap
{
    public class MaxHeap
    {
        private int this[int i]
        {
            get => _arr[i - 1];
            set => _arr[i - 1] = value;
        }
        
        public int[] GetInnerArray => _arr;

        private readonly int[] _arr;
        private int _heapSize;

        public MaxHeap(params int[] arr)
        {
            _arr = arr;
        }

        private int Parent(int i) => i >> 1;

        private int Left(int i) => i << 1;

        private int Right(int i) => (i << 1) + 1;

        private void Heapify(int parentIndex)
        {
            int leftIndex = Left(parentIndex);
            int rightIndex = Right(parentIndex);
            int largestIndex;

            if (leftIndex <= _heapSize && this[leftIndex] > this[parentIndex])
            {
                largestIndex = leftIndex;
            }
            else
            {
                largestIndex = parentIndex;
            }

            if (rightIndex <= _heapSize && this[rightIndex] > this[largestIndex])
            {
                largestIndex = rightIndex;
            }

            if (largestIndex != parentIndex)
            {
                SwapItems(parentIndex, largestIndex);
                Heapify(largestIndex);
            }
        }

        private void SwapItems(int l, int r)
        {
            int temp = this[l];
            this[l] = this[r];
            this[r] = temp;
        }

        public void Build()
        {
            _heapSize = _arr.Length;
            for (int i = _arr.Length / 2; i >= 1; i--)
            {
                Heapify(i);
            }
        }

        public void Sort()
        {
            Build();
            for (int i = _arr.Length; i >= 2; i--)
            {
                SwapItems(1, i);
                _heapSize = _heapSize - 1;
                Heapify(1);
            }
        }
    }
}
