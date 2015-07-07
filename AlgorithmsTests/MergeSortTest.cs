using System.Collections.Generic;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    internal class MergeSortTest : SortTest {
        [Test]
        public void TestMergeAsc() {
            var sequences = new[] {
                // edge cases
                new double[] { },
                new double[] { 1 },

                // warming up
                new double[] { 1, 2 },
                new double[] { 1, 2, 3},
                new double[] { 1, 2, 3, 4},

                // even
                new double[] { 1, 3, 5, 2, 4, 6 },
                new double[] { 2, 4, 6, 1, 3, 5 },
                new double[] { 1, 1, 3, 5, 5, 2, 4, 4, 6, 6},
                new double[] { 2, 2, 4, 4, 6, 1, 1, 3, 5, 5 },

                // odd
                new double[] { 1, 3, 5, 2, 4, 6, 7 },
                new double[] { 2, 4, 6, 1, 3, 5, 7 },
                new double[] { 1, 1, 3, 5, 5, 2, 4, 4, 6, 6, 6},
                new double[] { 2, 2, 4, 4, 6, 1, 1, 3, 3, 5, 5 }
            };

            foreach (var seq in sequences) {
                var buffer = new List<double>(seq.Length);
                MergeSort.Merge(seq, 0, seq.Length, buffer, Sorting.Asc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.LtOrEq));
            }
        }

        [Test]
        public void TestMergeDesc() {
            var sequences = new[] {
                // edge cases
                new double[] { },
                new double[] { 1 },

                // warming up
                new double[] { 1, 2 },
                new double[] { 3, 2, 1 },
                new double[] { 4, 3, 2, 1 },

                // even
                new double[] { 5, 3, 1, 6, 4, 2 },
                new double[] { 6, 4, 2, 5, 3, 1 },
                new double[] { 5, 5, 3, 1, 1, 6, 4, 4, 2, 2 },
                new double[] { 6, 6, 4, 4, 2, 5, 5, 3, 1, 1 },

                // odd
                new double[] { 5, 3, 1, 7, 6, 4, 2 },
                new double[] { 6, 4, 2, 7, 5, 3, 1},
                new double[] { 5, 5, 3, 1, 1, 6, 4, 4, 2, 2, 2 },
                new double[] { 6, 4, 4, 2, 2, 5, 5, 3, 3, 1, 1 }
            };

            foreach (var seq in sequences) {
                var buffer = new List<double>(seq.Length);
                MergeSort.Merge(seq, 0, seq.Length, buffer, Sorting.Desc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.GtOrEq));
            }
        }

        [Test]
        public void TestTopDownSortAsc() {
            TestSortAsc(MergeSort.SortTopDown);
        }

        [Test]
        public void TestTopDownSortDesc() {
            TestSortDesc(MergeSort.SortTopDown);
        }

        [Test]
        [Ignore]
        public void TestBottomUpSortAsc() {
            TestSortAsc(MergeSort.SortBottomUp);
        }

        [Test]
        [Ignore]
        public void TestBottomUpSortDesc() {
            TestSortDesc(MergeSort.SortBottomUp);
        }
    }
}
