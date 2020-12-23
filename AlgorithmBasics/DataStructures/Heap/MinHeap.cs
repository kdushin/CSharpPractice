using System;

namespace AlgorithmBasics.DataStructures.Heap
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private T[] _arr;
        private int _heapSize;

        private T this[int i]
        {
            get => _arr[i - 1];
            set => _arr[i - 1] = value;
        }

        private int Parent(int i) => i >> 1;

        private int Left(int i) => i << 1;

        private int Right(int i) => (i << 1) + 1;

        public MinHeap(int size)
        {
            _arr = new T[size];
            _heapSize = 0;
        }
        
        public MinHeap(params T[] arr)
        {
            _arr = new T[arr.Length];
            foreach (var item in arr)
            {
                Insert(item);
            }
        }

        public void Insert(T item)
        {
            if (_heapSize == 0)
            {
                _heapSize = 1;
                this[1] = item;
            }
            else
            {
                _heapSize++;
                if (_arr.Length < _heapSize)
                {
                    // create new array twice bigger than previous one
                    Array.Resize(ref _arr, 2 * (_arr.Length + 1));
                }
                this[_heapSize] = item;
                HeapifyUp(_heapSize);
            }
        }

        public T ExtractMin()
        {
            T result = this[1];
            if (_heapSize < 1) { return default; }
            
            this[1] = this[_heapSize--];
            HeapifyDown(1);
            
            return result;
        }
        
        private void HeapifyUp(int itemIndex)
        {
            if (itemIndex != 1)
            {
                int parentIndex = Parent(itemIndex);
                if (this[itemIndex].CompareTo(this[parentIndex]) < 0)
                {
                    SwapItems(parentIndex, itemIndex);
                    HeapifyUp(parentIndex);
                }
            }
        }

        private void HeapifyDown(int parentIndex)
        {
            int minIndex = parentIndex;
            while (true)
            {
                int leftChildIndex = Left(parentIndex);
                int rightChildIndex = Right(parentIndex);
                
                if (leftChildIndex <= _heapSize && 
                    this[leftChildIndex].CompareTo(this[minIndex]) < 0)
                {
                    minIndex = leftChildIndex;
                }
                if (rightChildIndex <= _heapSize && 
                    this[rightChildIndex].CompareTo(this[minIndex]) < 0)
                {
                    minIndex = rightChildIndex;
                }
                if (minIndex == parentIndex)
                {
                    return;
                }
            
                SwapItems(parentIndex, minIndex);
                parentIndex = minIndex;
            }
        }

        private void SwapItems(int l, int r)
        {
            T temp = this[l];
            this[l] = this[r];
            this[r] = temp;
        }
    }
}