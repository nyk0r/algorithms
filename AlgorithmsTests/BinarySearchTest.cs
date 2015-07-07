using System.Linq;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    public class BinarySearchTest {
        [Test]
        public void FindNormalAsc() {
            Assert.AreEqual(BinarySearch.Find(new double[] { 1, 2, 3, 4, 5, 6 }, 2), 1);
            Assert.AreEqual(BinarySearch.Find(new int[] { 1, 2, 3, 4, 5, 6, 7, 18, 19, 21, 100 }, 18), 7);

            Assert.AreEqual(BinarySearch.Find(new double[] { 1, 2, 3, 4, 5, 6 }, 1), 0);
            Assert.AreEqual(BinarySearch.Find(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 7), 6);

            Assert.AreEqual(BinarySearch.Find(new int[] { 1 }, 1), 0);
        }

        [Test]
        public void FindNormalDesc() {
            Assert.AreEqual(BinarySearch.Find(new double[] { 6, 5, 4, 3, 2, 1 }, 5, Sorting.Desc), 1);
            Assert.AreEqual(BinarySearch.Find(new int[] { 100, 21, 19, 18, 7, 6, 5, 4, 3, 2, 1 }, 2, Sorting.Desc), 9);

            Assert.AreEqual(BinarySearch.Find(new double[] { 6, 5, 4, 3, 2, 1 }, 6, Sorting.Desc), 0);
            Assert.AreEqual(BinarySearch.Find(new int[] { 7, 6, 5, 4, 3, 2, 1 }, 1, Sorting.Desc), 6);

            Assert.AreEqual(BinarySearch.Find(new int[] { 1 }, 1, Sorting.Desc), 0);
        }

        [Test]
        public void FindNotExisting() {
            var seq1 = new double[] { 1, 2, 3, 4, 5, 6 };
            Assert.AreEqual(BinarySearch.Find(seq1, 0), -1);
            Assert.AreEqual(BinarySearch.Find(seq1, 0, Sorting.Desc), -1);
            Assert.AreEqual(BinarySearch.Find(seq1.Reverse().ToList(), 0, Sorting.Desc), -1);
            var seq2 = new int[] { 1, 2, 3, 4, 5, 6, 7 };
            Assert.AreEqual(BinarySearch.Find(seq2, 8), -1);
            Assert.AreEqual(BinarySearch.Find(seq2, 8, Sorting.Desc), -1);
            Assert.AreEqual(BinarySearch.Find(seq2.Reverse().ToList(), 8, Sorting.Desc), -1);

            var seq3 = new double[] { 1, 2, 5, 6, 11, 12, 13, 14, 15 };
            Assert.AreEqual(BinarySearch.Find(seq3, 3), -1);
            Assert.AreEqual(BinarySearch.Find(seq3, 3, Sorting.Desc), -1);
            Assert.AreEqual(BinarySearch.Find(seq3.Reverse().ToList(), 3, Sorting.Desc), -1);
            var seq4 = new int[] { 1, 2, 3, 4, 5, 6, 8, 9, 12 };
            Assert.AreEqual(BinarySearch.Find(seq4, 7), -1);
            Assert.AreEqual(BinarySearch.Find(seq4, 7, Sorting.Desc), -1);
            Assert.AreEqual(BinarySearch.Find(seq4.Reverse().ToList(), 7, Sorting.Desc), -1);

            Assert.AreEqual(BinarySearch.Find(new int[] { 1 }, 2), -1);
            Assert.AreEqual(BinarySearch.Find(new int[] { 1 }, 2, Sorting.Desc), -1);
            Assert.AreEqual(BinarySearch.Find(new int[] { }, 2), -1);
            Assert.AreEqual(BinarySearch.Find(new int[] { }, 2, Sorting.Desc), -1);
        }
    }
}
