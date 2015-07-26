using System.Linq;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    internal class BinaryHeapTest {
        [Test]
        public void TestMaxHeap() {
            TestHeap(BinaryHeapType.Max);
        }

        [Test]
        public void TestMinHeap() {
            TestHeap(BinaryHeapType.Min);
        }

        private static void TestHeap(BinaryHeapType type) {
            var fixture1 = new int[] { 0, 0, 1, 2, 3, 4, 5, 5, 5, 6, 7, 8, 9 };
            var fixture2 = new int[] { 9, 9, 9, 2, 4, 5, 10, 15, 0, 8, 13, 19 };
            SequenceUtils.Shuffle(fixture1);
            var heap = new BinaryHeap<int>(fixture1, type);
            foreach (var item in fixture2) {
                heap.Add(item);
            }
            var combined = fixture1.Concat(fixture2).ToArray();
            combined = type == BinaryHeapType.Max
                ? combined.OrderByDescending(a => a).ToArray()
                : combined.OrderBy(a => a).ToArray();
            Assert.AreEqual(combined.Count(), heap.Count);
            foreach (var item in combined) {
                Assert.AreEqual(item, heap.GetPeak());
            }
            Assert.AreEqual(heap.Count, 0);
        }
    }
}
