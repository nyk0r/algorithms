using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Algorithms;
using NUnit.Framework;

namespace AlgorithmsTests {
    internal class SortTest {
        protected delegate void SortAlgorithm<T>(IList<T> seq, Sorting dir) where T : IComparable<T>;

        protected struct PositionedValue : IComparable<PositionedValue> {
            public double Value { get; private set; }
            public int Position { get; private set; }

            public PositionedValue(double value, int position) : this() {
                Value = value;
                Position = position;
            }

            public int CompareTo(PositionedValue other) {
                return Value.CompareTo(other.Value);
            }
        }

        private static readonly RNGCryptoServiceProvider RngCsp = new RNGCryptoServiceProvider();

        protected static PositionedValue[][] GetFixtures() {
            var arr1 = new byte[100];
            var arr2 = new byte[99];
            RngCsp.GetNonZeroBytes(arr1);
            RngCsp.GetNonZeroBytes(arr2);


            return new[] {
                // edge cases
                new double[] { },
                new double[] { 1 },

                // warming up
                new double[] { 1, 2 },
                new double[] { 2, 1 },
                new double[] { 3, 1, 2 },
                new double[] { 4, 2, 3, 5 },

                // even
                arr1.Select(a => (double)a).ToArray(),
                new double[] { 4, 2, 3, 5, 5, 4, 2, 3 },

                // odd
                arr2.Select(a => (double)a).ToArray(),
                new double[] { 4, 2, 3, 5, 4, 2, 3 },
            }.Select(arr => arr.Select((a, i) => new PositionedValue(a, i)).ToArray()).ToArray();
        }

        private static void TestStability(IEnumerable<PositionedValue> seq) {
            Assert.True(SequenceUtils.CheckNeighbours(seq, (a, b) => {
                if (Math.Abs(a.Value - b.Value) > float.Epsilon) {
                    return true;
                }
                return a.Position < b.Position;
            }));
        }

        private static Dictionary<PositionedValue, int> GetHistogram(IEnumerable<PositionedValue> seq) {
            var result = new Dictionary<PositionedValue, int>();
            foreach (var elem in seq) {
                int count;
                if (result.TryGetValue(elem, out count)) {
                    result[elem] = count + 1;
                } else {
                    result.Add(elem, 1);
                }
            }
            return result;
        }

        private static bool CompareHistograms(Dictionary<PositionedValue, int> initial, Dictionary<PositionedValue, int> result) {
            foreach (var key in initial.Keys) {
                if (!result.ContainsKey(key)) {
                    return false;
                }
                if (initial[key] != result[key]) {
                    return false;
                }
            }
            return true;
        }

        private static void TestSort(SortAlgorithm<PositionedValue> algorithm, Sorting sortingDirection, Ordering resultOrdring, bool shouldBeStable)
        {
            foreach (var seq in GetFixtures()) {
                var initialHist = GetHistogram(seq);
                algorithm(seq, sortingDirection);
                // values are the same
                Assert.True(CompareHistograms(initialHist, GetHistogram(seq)));
                // values are sorted
                Assert.True(SequenceUtils.IsOrdered(seq, resultOrdring));
                // sorting is stable
                if (shouldBeStable) {
                    TestStability(seq);
                }
            }
        }

        protected static void TestSortAsc(SortAlgorithm<PositionedValue> algorithm, bool shouldBeStable = false) {
            TestSort(algorithm, Sorting.Asc, Ordering.LtOrEq, shouldBeStable);
        }

        protected static void TestSortDesc(SortAlgorithm<PositionedValue> algorithm, bool shouldBeStable = false) {
            TestSort(algorithm, Sorting.Desc, Ordering.GtOrEq, shouldBeStable);
        }
    }
}
