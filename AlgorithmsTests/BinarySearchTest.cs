using System;
using System.Collections.Generic;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    public class BinarySearchTest {
        [Test]
        public void FindNormal() {
            Assert.AreEqual(BinarySearch.Find(new double[] { 1, 2, 3, 4, 5, 6 }, 2), 1);
            Assert.AreEqual(BinarySearch.Find(new int[] { 1, 2, 3, 4, 5, 6, 7, 18, 19, 21, 100 }, 18), 7);

            Assert.AreEqual(BinarySearch.Find(new double[] { 1, 2, 3, 4, 5, 6 }, 1), 0);
            Assert.AreEqual(BinarySearch.Find(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 7), 6);

            Assert.AreEqual(BinarySearch.Find(new int[] { 1 }, 1), 0);
        }

        [Test]
        public void FindNotExisting() {
            Assert.AreEqual(BinarySearch.Find(new double[] { 1, 2, 3, 4, 5, 6 }, 0), -1);
            Assert.AreEqual(BinarySearch.Find(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 8), -1);

            Assert.AreEqual(BinarySearch.Find(new double[] { 1, 2, 5, 6, 11, 12, 13, 14, 15 }, 3), -1);
            Assert.AreEqual(BinarySearch.Find(new int[] { 1, 2, 3, 4, 5, 6, 8, 9, 12 }, 7), -1);

            Assert.AreEqual(BinarySearch.Find(new int[] { 1 }, 2), -1);
            Assert.AreEqual(BinarySearch.Find(new int[] { }, 2), -1);
        }
    }
}
