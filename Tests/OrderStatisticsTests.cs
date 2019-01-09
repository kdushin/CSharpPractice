using CSharpPractice.Algorithms;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class OrderStatisticsTests
    {
        [Test]
        public void TestRandomSelectSecondSmallestItemEven()
        {
            var inputArray = new[] {0, 9, 1, 3, 5, 7};
            var actualResult = OrderStatistics.RSelect(inputArray, 0, inputArray.Length - 1, 2);
            Assert.That(actualResult, Is.EqualTo(1));
        }
        
        [Test]
        public void TestRandomSelectSecondSmallestItemOdd()
        {
            var inputArray = new[] {0, 9, 1, 3, 5, 7, 7};
            var actualResult = OrderStatistics.RSelect(inputArray, 0, inputArray.Length - 1, 2);
            Assert.That(actualResult, Is.EqualTo(1));
        }
        
        [Test]
        public void TestRandomSelectWithOneItemInArray()
        {
            var inputArray = new[] {0};
            var actualResult = OrderStatistics.RSelect(inputArray, 0, inputArray.Length - 1, 2);
            Assert.That(actualResult, Is.EqualTo(0));
        }
        
        [Test]
        public void TestRandomSelectLargestElement()
        {
            var inputArray = new[] {0, 5, 1};
            var actualResult = OrderStatistics.RSelect(inputArray, 0, inputArray.Length - 1, 3);
            Assert.That(actualResult, Is.EqualTo(5));
        }
    }
}