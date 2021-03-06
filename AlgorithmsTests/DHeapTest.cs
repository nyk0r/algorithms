﻿using System.Linq;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    internal class DHeapTest {
        [Test]
        public void TestMaxHeap() {
            TestHeap(DHeapType.Max, 1);
            TestHeap(DHeapType.Max, 2);
            TestHeap(DHeapType.Max, 3);
            TestHeap(DHeapType.Max, 4);
            TestHeap(DHeapType.Max, 5);
        }

        [Test]
        public void TestMinHeap() {
            TestHeap(DHeapType.Min, 1);
            TestHeap(DHeapType.Min, 2);
            TestHeap(DHeapType.Min, 3);
            TestHeap(DHeapType.Min, 4);
            TestHeap(DHeapType.Min, 5);
        }

        private static void TestHeap(DHeapType type, int rank) {
            var fixture1 = new [] { 0, 0, 1, 2, 3, 4, 5, 5, 5, 6, 7, 8, 9 };
            var fixture2 = new [] { 9, 9, 9, 2, 4, 5, 10, 15, 0, 8, 13, 19 };
            SequenceUtils.Shuffle(fixture1);
            var heap = new DHeap<int>(fixture1, type, rank);
            foreach (var item in fixture2) {
                heap.Add(item);
            }
            var combined = fixture1.Concat(fixture2).ToArray();
            combined = type == DHeapType.Max
                ? combined.OrderByDescending(a => a).ToArray()
                : combined.OrderBy(a => a).ToArray();
            Assert.AreEqual(combined.Count(), heap.Count);
            foreach (var item in combined) {
                Assert.AreEqual(item, heap.GetPeak());
            }
            Assert.AreEqual(0, heap.Count);
        }
    }
}
