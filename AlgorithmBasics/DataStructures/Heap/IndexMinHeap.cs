namespace AlgorithmBasics.DataStructures.Heap
{
    // TODO: consider to change _heapIndexes, _heapArray, _keys to dictionaries in order to maintain generic Index
    public class IndexMinHeap<TKey> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Array used to store heap indexes.
        /// _heapIndexes[_heapArray[i]] = _heapArray[_heapIndexes[i]] = i 
        /// </summary>
        private readonly int[] _heapIndexes; 

        /// <summary>
        /// heap array 1-based indexed.
        /// </summary>
        private readonly int[] _heapArray;
        
        /// <summary>
        /// Keys collection: builds min heap structure by comparing these keys.
        /// </summary>
        private readonly TKey[] _keys;

        private int _heapSize;
        private readonly int _heapMaxCount;
        
        private int Parent(int i) => i >> 1;
        
        private int Left(int i) => i << 1;
        
        private int Right(int i) => (i << 1) + 1;

        public IndexMinHeap(int itemsHeapMaxNumber)
        {
            if (_heapMaxCount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _heapMaxCount = itemsHeapMaxNumber;
            _heapSize = 0;
            _keys = new TKey[itemsHeapMaxNumber + 1];
            _heapArray = new int[_heapMaxCount + 1];
            _heapIndexes = new int[_heapMaxCount + 1];
            for (int i = 0; i <= _heapMaxCount; i++)
            {
                _heapIndexes[i] = -1;
            }
        }
        
        /// <summary>
        /// Returns true if min heap is empty.
        /// </summary>
        public bool IsEmpty() => _heapSize == 0;
        
        /// <summary>
        /// Returns true if contains item with specified index.
        /// </summary>
        public bool Contains(int i) 
        {
            ValidateIndex(i);
            return _heapIndexes[i] != -1;
        }
        
        /// <summary>
        /// Inserts item's key with associated index.
        /// </summary>
        public void Insert(int i, TKey key) 
        {
            ValidateIndex(i);
            if (Contains(i))
            {
                throw new InvalidOperationException("index is already in the min heap");
            }
            _heapSize++;
            _heapIndexes[i] = _heapSize;
            _heapArray[_heapSize] = i;
            _keys[i] = key;
            HeapifyUp(_heapSize);
        }

        /// <summary>
        /// Get heap's minimum index.
        /// </summary>
        /// <returns></returns>
        public int GetMin()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Min heap is empty");
            }

            return _heapArray[1];
        }
        
        /// <summary>
        /// Get heap's minimum index. Removes it. Restores min heap structure.
        /// </summary>
        public int ExtractMin()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Min heap is empty");
            }
            int min = _heapArray[1];
            SwapItems(1, _heapSize);
            _heapSize--;
            
            HeapifyDown(1);
            _heapIndexes[min] = -1;
            _keys[min] = default;
            
            return min;    
        }
        
        /// <summary>
        /// Returns key associated with specified item's index.
        /// </summary>
        public TKey GetItemKey(int i)
        {
            ValidateIndex(i);
            if (!Contains(i))
            {
                throw new InvalidOperationException("Index wasn't found in min heap");
            }
            
            return _keys[i];
        }
        
        /// <summary>
        /// Deletes the key associated with specified index i. Restores min heap structure.
        /// </summary>
        public void Delete(int i) 
        {
            ValidateIndex(i);
            if (!Contains(i))
            {
                throw new InvalidOperationException("Specified index wasn't found in min heap");
            }
            int index = _heapIndexes[i];
            SwapItems(index, _heapSize);
            _heapSize--;
            HeapifyUp(index);
            HeapifyDown(index);
            _keys[i] = default;
            _heapIndexes[i] = -1;
        }
        
        /// <summary>
        /// Decrease key for item with specified index i.
        /// </summary>
        public void DecreaseKey(int i, TKey key) 
        {
            ValidateIndex(i);
            if (!Contains(i))
            {
                throw new InvalidOperationException("Index wasn't found in min heap.");
            }
            if (_keys[i].CompareTo(key) <= 0)
            {
                throw new InvalidOperationException($"Current key equals {_keys[i]}. New key equals {key}");
            }
            _keys[i] = key;
            HeapifyUp(_heapIndexes[i]);
        }
        
        private void ValidateIndex(int i) 
        {
            if (i < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (i > _heapMaxCount)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        
        private void HeapifyUp(int index) 
        {
            while (index != 1)
            {
                int parent = Parent(index);
            
                if (IsLesser(index, Parent(index)))
                {
                    SwapItems(parent, index);
                    index = parent;
                }
                else
                {
                    break;
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
                if (leftChildIndex <= _heapSize && IsLesser(leftChildIndex, parentIndex))
                {
                    minIndex = leftChildIndex;
                }
                if (rightChildIndex <= _heapSize && IsLesser(rightChildIndex, minIndex))
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

        /// <summary>
        /// Returns true if firstIndex key is less than secondIndex key.
        /// </summary>
        private bool IsLesser(int firstIndex, int secondIndex)
        {
            return _keys[_heapArray[firstIndex]].CompareTo(_keys[_heapArray[secondIndex]]) < 0;
        }

        private void SwapItems(int first, int second) 
        {
            int temp = _heapArray[first];
            _heapArray[first] = _heapArray[second];
            _heapArray[second] = temp;
            
            _heapIndexes[_heapArray[first]] = first;
            _heapIndexes[_heapArray[second]] = second;
        }
    }
}