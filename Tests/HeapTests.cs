using System.Collections;
using System.Collections.Generic;
using AlgorithmBasics;
using AlgorithmBasics.DataStructures;
using AlgorithmBasics.DataStructures.Heap;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class HeapTests
    {
        [Test]
        public void HeapTest()
        {
            var heap = new MaxHeap(4, 1, 3, 2, 16, 9, 10, 14, 8, 7);
            heap.Build();
            var expectedArr = new[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1};
            
            //Assert Binary heap structure
            Assert.That(heap.GetInnerArray, Is.EquivalentTo(expectedArr));
            
            expectedArr = new[] {16, 14, 10, 9, 8, 7, 4, 3, 2, 1};
            heap.Sort();
            
            //Assert Binary heap sort
            Assert.That(heap.GetInnerArray, Is.EquivalentTo(expectedArr));
        }

        [Test]
        public void TestMinHeap()
        {
            var ints = new[] {4, 1, 3, 2, 16, 9, 10, 14, 8, 7};
            var heap = new MinHeap<int>(ints);

            var actualList = new List<int>();
            int minItem = int.MaxValue;
            while (true)
            {
                minItem = heap.ExtractMin();
                if (minItem == default) { break; }
                
                actualList.Add(minItem);
            }
            
            int[] expectedArr = new[] {1, 2, 3, 4, 7, 8, 9, 10, 14, 16};
            Assert.That(actualList.ToArray(), Is.EquivalentTo(expectedArr));
        }
    }
}
