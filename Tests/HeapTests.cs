using CSharpPractice.DataStructures;
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
            heap.Sort();
            //TODO: assert me!!!
        }

    }
}
