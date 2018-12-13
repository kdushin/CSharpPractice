using CSharpPractice.Algorithms.BinarySearch;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BinarySearchTests
    {
        [Test]
        public void TestBinarySearch()
        {
            var arr = new[] { 2, 19, 39, 44, 78, 87, 90, 444, 1900, 9999 };
            var target = 87;
            int targetIndex = BinarySearch.FindItem(arr, target);
            Assert.That(target, Is.EqualTo(arr[targetIndex]));
        }

        [Test]
        public void TestBinarySearchItemNotFound()
        {
            var arr = new [] { 2, 19, 39, 44, 78, 87, 90, 444, 1900, 9999 };
            var target = 13;
            int targetIndex = BinarySearch.FindItem(arr, target);
            Assert.That(-1, Is.EqualTo(targetIndex));
        }
    }
}
