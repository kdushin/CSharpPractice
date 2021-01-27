using System;
using System.Collections.Generic;

namespace AlgorithmBasics.DataStructures.Heap
{
    public interface IIndexable<T>
    {
        int GetIndex();
    }
    
    public class MinHeap<T> where T : IComparable<T>, IIndexable<T>
    {
        private T[] _arr;
        private int _heapSize;
        private readonly Dictionary<int, int> _indexMap = new Dictionary<int, int>();

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

        public bool IsEmpty()
        {
            return _heapSize <= 0;
        }

        public void Insert(T item)
        {
            if (_heapSize == 0)
            {
                _heapSize = 1;
                this[1] = item;
                AddOrUpdateMap(item, 1);
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
                AddOrUpdateMap(item, _heapSize);
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
            while (true)
            {
                if (itemIndex != 1)
                {
                    int parentIndex = Parent(itemIndex);
                    if (this[itemIndex].CompareTo(this[parentIndex]) < 0)
                    {
                        SwapItems(parentIndex, itemIndex);
                        itemIndex = parentIndex;
                        continue;
                    }
                }

                break;
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

        public void SwapItems(int l, int r)
        {
            T leftTemp = this[l];
            this[l] = this[r];
            this[r] = leftTemp;

            _indexMap[this[l].GetIndex()] = l;
            _indexMap[this[r].GetIndex()] = r;
        }

        public T Delete(int index)
        {
            if (_heapSize == 0) { return default; }
            
            T result = default;
            
            if (_indexMap.TryGetValue(index, out int heapIndex))
            {
                _indexMap.Remove(index);

                result = this[heapIndex];
                this[heapIndex] = this[_heapSize];
                _heapSize--;
                
                if (heapIndex == 1 || this[Parent(heapIndex)].CompareTo(this[heapIndex]) < 0)
                {
                    HeapifyDown(heapIndex);
                }
                else
                {
                    HeapifyUp(heapIndex);
                }
            }

            return result;
        }

        private void AddOrUpdateMap(T item, int arrayIndex)
        {
            if (_indexMap.ContainsKey(item.GetIndex()))
            {
                _indexMap[item.GetIndex()] = arrayIndex;
            }
            else
            {
                _indexMap.Add(item.GetIndex(), arrayIndex);
            }
        }
    }
}