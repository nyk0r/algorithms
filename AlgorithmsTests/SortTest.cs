using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Algorithms;
using NUnit.Framework;

namespace AlgorithmsTests {
    internal delegate void SortAlgorithm<T>(IList<T> seq, Sorting dir) where T : IComparable<T>;

    internal class SortTest {
        private static readonly RNGCryptoServiceProvider RngCsp = new RNGCryptoServiceProvider();

        protected static double[][] GetFixtures() {
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
                new double[] { 3, 1, 2 },
                new double[] { 4, 2, 3, 5 },

                // even
                arr1.Select(a => (double)a).ToArray(),

                // odd
                arr2.Select(a => (double)a).ToArray()
            };
        }

        protected static void TestSortAsc(SortAlgorithm<double> algorithm) {
            foreach (var seq in GetFixtures()) {
                algorithm(seq, Sorting.Asc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.LtOrEq));
            }
        }

        protected static void TestSortDesc(SortAlgorithm<double> algorithm) {
            foreach (var seq in GetFixtures()) {
                algorithm(seq, Sorting.Desc);
                Assert.True(SequenceUtils.IsOrdered(seq, Ordering.GtOrEq));
            }
        }
    }
}
