using System;
using System.Collections.Generic;
using System.Linq;
using CSharpPractice.Algorithms.Sorting;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SortingTests
    {
        [Test]
        public void TestInsertionSort()
        {
            var expectedArray = new[] {7, 9, 11, 22, 42, 88, 99};
            var actualArray = new[] {22, 11, 99, 88, 9, 7, 42};
            Insertion.Sort(actualArray);
            Assert.That(actualArray, Is.EqualTo(expectedArray));
        }

        [Test]
        public void TestMergeSortEven()
        {
            var unsortedArray = new int[] {1, 3, 2, 4, 5, 8, 6, 7};
            var sortedArray = new int[] {1, 2, 3, 4, 5, 6, 7, 8};
            Merge.Sort(unsortedArray);
            Assert.That(unsortedArray, Is.EqualTo(sortedArray));
        }

        [Test]
        public void TestMergeSortOdd()
        {
            var unsortedArray = new int[] {1, 3, 2, 11, 4, 5, 8, 6, 7};
            var sortedArray = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 11};
            Merge.Sort(unsortedArray);
            Assert.That(unsortedArray, Is.EqualTo(sortedArray));
        }

        [Test]
        public void TestMergeSortRepeatedNumbers()
        {
            var unsortedArray = new int[] {1, 3, 2, 4, 5, 8, 2, 6, 1, 7};
            var sortedArray = new int[] {1, 1, 2, 2, 3, 4, 5, 6, 7, 8};
            Merge.Sort(unsortedArray);
            Assert.That(unsortedArray, Is.EqualTo(sortedArray));
        }

        [Test]
        public void TestSelectionSortNull()
        {
            Assert.Throws<ArgumentNullException>(() => Selection.Sort(null));
        }

        [Test]
        public void TestSelectionSort()
        {
            var unsortedArray = new[] {22, 11, 99, 88, 9, 7, 42};
            var sortedArray = new[] {7, 9, 11, 22, 42, 88, 99};
            Selection.Sort(unsortedArray);
            Assert.That(unsortedArray, Is.EqualTo(sortedArray));
        }

        [Test]
        public void TestSelectionSortTwoItems()
        {
            var actualArray = new[] {99, 5};
            var expectedArray = new[] {5, 99};
            Selection.Sort(actualArray);
            Assert.That(actualArray, Is.EqualTo(expectedArray));
        }

        [Test]
        public void TestSelectionSortSameNumbers()
        {
            var actualArray = new[] {22, 22, 3, 5, 66, 66, 90, 3, 5};
            var expectedArray = new[] {3, 3, 5, 5, 22, 22, 66, 66, 90};
            Selection.Sort(actualArray);
            Assert.That(actualArray, Is.EqualTo(expectedArray));
        }

        [Test]
        public void TestCountNumberOfInversions()
        {
            var inputArr = new [] {3, 7, 8, 2, 6, 7};
            Assert.That(NumberOfInversions.Count(inputArr), Is.EqualTo(6), "Wrong number of inversions in input array were counted");
            Assert.That(NumberOfInversions.Count(inputArr), Is.EqualTo(6), "Wrong number of inversions in input array were counted");
            Assert.That(inputArr, Is.EqualTo(new [] {2, 3, 6, 7, 7, 8}), "Input array wasn't sorted correctly");
        }

        [Test]
        public void TestCountNumberOfInversionsOdd()
        {
            var inputArr = new [] {3, 7, 8, 2, 6, 7, 1};
            Assert.That(NumberOfInversions.Count(inputArr), Is.EqualTo(12), "Wrong number of inversions in input array were counted");
            Assert.That(inputArr, Is.EqualTo(new [] {1, 2, 3, 6, 7, 7, 8}), "Input array wasn't sorted correctly");
        }

        [Test]
        public void TestQuickSort()
        {
            var inputArr = new [] {3, 7, 8, 2, 6, 7};
            Quick.Sort(inputArr);
        }
    }
}