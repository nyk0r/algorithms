using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    internal class MergeSortTest {
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
                var buffer = new double[seq.Length];
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
                var buffer = new double[seq.Length];
                MergeSort.Merge(seq, 0, seq.Length, buffer, Sorting.Desc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.GtOrEq));
            }
        }

        private static double[][] GetFixtures() {
            var rnd = new Random();
            return new [] {
                // edge cases
                new double[] { },
                new double[] { 1 },

                // warming up
                new double[] { 1, 2 },
                new double[] { 3, 1, 2 },
                new double[] { 4, 2, 3, 5 },

                // even
                Enumerable.Range(0, 100).Select(_ => Math.Round(rnd.NextDouble() * 100)).ToArray(),
                
                // odd
                Enumerable.Range(0, 99).Select(_ => Math.Round(rnd.NextDouble() * 100)).ToArray()
            };
        }

        [Test]
        public void TestTopDownSortAsc() {
            foreach (var seq in GetFixtures()) {
                MergeSort.SortTopDown(seq, Sorting.Asc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.LtOrEq));
            }
        }

        [Test]
        public void TestTopDownSortDsc() {
            foreach (var seq in GetFixtures()) {
                MergeSort.SortTopDown(seq, Sorting.Desc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.GtOrEq));
            }
        }

        [Test]
        [Ignore]
        public void TestBottomUpSortAsc() {
            foreach (var seq in GetFixtures()) {
                MergeSort.SortBottomUp(seq, Sorting.Asc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.LtOrEq));
            }
        }

        [Test]
        [Ignore]
        public void TestBottomUpSortDsc()
        {
            foreach (var seq in GetFixtures()) {
                MergeSort.SortBottomUp(seq, Sorting.Desc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.GtOrEq));
            }
        }
    }
}
