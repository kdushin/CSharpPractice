using AlgorithmBasics.DataStructures;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class HeapTests
    {
        [Test]
        public void HeapTest()
        {
            var heap = new BinaryHeap(4, 1, 3, 2, 16, 9, 10, 14, 8, 7);
            heap.Build();
            var expectedArr = new[] {16, 14, 10, 8, 7, 9, 3, 2, 4, 1};
            
            //Assert Binary heap structure
            Assert.That(heap.GetInnerArray, Is.EquivalentTo(expectedArr));
            
            expectedArr = new[] {16, 14, 10, 9, 8, 7, 4, 3, 2, 1};
            heap.Sort();
            
            //Assert Binary heap sort
            Assert.That(heap.GetInnerArray, Is.EquivalentTo(expectedArr));
        }
    }
}
