using System.Linq;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    public class BinarySearchTest {
        [Test]
        public void FindNormalAsc() {
            Assert.AreEqual(1, BinarySearch.Find(new double[] { 1, 2, 3, 4, 5, 6 }, 2));
            Assert.AreEqual(7, BinarySearch.Find(new [] { 1, 2, 3, 4, 5, 6, 7, 18, 19, 21, 100 }, 18));

            Assert.AreEqual(0, BinarySearch.Find(new double[] { 1, 2, 3, 4, 5, 6 }, 1));
            Assert.AreEqual(6, BinarySearch.Find(new [] { 1, 2, 3, 4, 5, 6, 7 }, 7));

            Assert.AreEqual(0, BinarySearch.Find(new [] { 1 }, 1));
        }

        [Test]
        public void FindNormalDesc() {
            Assert.AreEqual(1, BinarySearch.Find(new double[] { 6, 5, 4, 3, 2, 1 }, 5, Sorting.Desc));
            Assert.AreEqual(9, BinarySearch.Find(new [] { 100, 21, 19, 18, 7, 6, 5, 4, 3, 2, 1 }, 2, Sorting.Desc));

            Assert.AreEqual(0, BinarySearch.Find(new double[] { 6, 5, 4, 3, 2, 1 }, 6, Sorting.Desc));
            Assert.AreEqual(6, BinarySearch.Find(new [] { 7, 6, 5, 4, 3, 2, 1 }, 1, Sorting.Desc));

            Assert.AreEqual(0, BinarySearch.Find(new [] { 1 }, 1, Sorting.Desc));
        }

        [Test]
        public void FindNotExisting() {
            var seq1 = new double[] { 1, 2, 3, 4, 5, 6 };
            Assert.AreEqual(-1, BinarySearch.Find(seq1, 0));
            Assert.AreEqual(-1, BinarySearch.Find(seq1, 0, Sorting.Desc));
            Assert.AreEqual(-1, BinarySearch.Find(seq1.Reverse().ToList(), 0, Sorting.Desc));

            var seq2 = new [] { 1, 2, 3, 4, 5, 6, 7 };
            Assert.AreEqual(-1, BinarySearch.Find(seq2, 8));
            Assert.AreEqual(-1, BinarySearch.Find(seq2, 8, Sorting.Desc));
            Assert.AreEqual(-1, BinarySearch.Find(seq2.Reverse().ToList(), 8, Sorting.Desc));

            var seq3 = new double[] { 1, 2, 5, 6, 11, 12, 13, 14, 15 };
            Assert.AreEqual(-1, BinarySearch.Find(seq3, 3));
            Assert.AreEqual(-1, BinarySearch.Find(seq3, 3, Sorting.Desc));
            Assert.AreEqual(-1, BinarySearch.Find(seq3.Reverse().ToList(), 3, Sorting.Desc));

            var seq4 = new [] { 1, 2, 3, 4, 5, 6, 8, 9, 12 };
            Assert.AreEqual(-1, BinarySearch.Find(seq4, 7));
            Assert.AreEqual(-1, BinarySearch.Find(seq4, 7, Sorting.Desc));
            Assert.AreEqual(-1, BinarySearch.Find(seq4.Reverse().ToList(), 7, Sorting.Desc));

            Assert.AreEqual(-1, BinarySearch.Find(new [] { 1 }, 2));
            Assert.AreEqual(-1, BinarySearch.Find(new [] { 1 }, 2, Sorting.Desc));
            Assert.AreEqual(-1, BinarySearch.Find(new int[] { }, 2));
            Assert.AreEqual(-1, BinarySearch.Find(new int[] { }, 2, Sorting.Desc));
        }
    }
}
