using System;
using System.Linq;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    internal class SequenceUtilsTest {
        [Test]
        public void TestIsOrdered() {
            var ltSeq = new double[] {1, 2, 3, 4, 5};
            var ltEqSeq = new long[] {1, 2, 2, 3, 5};
            var gtSeq = new int[] {5, 3, 2, 1, 0};
            var gtEqSeq = new byte[] {5, 5, 2, 2, 0};
            var seq = new decimal[] {1, 100, 0, -1, 20, 1};

            Assert.True(SequenceUtils.IsOrdered(ltSeq, Ordering.Lt));
            Assert.True(SequenceUtils.IsOrdered(ltSeq, Ordering.LtOrEq));

            Assert.True(SequenceUtils.IsOrdered(gtSeq, Ordering.Gt));
            Assert.True(SequenceUtils.IsOrdered(gtSeq, Ordering.GtOrEq));

            Assert.False(SequenceUtils.IsOrdered(gtEqSeq, Ordering.Gt));
            Assert.False(SequenceUtils.IsOrdered(ltEqSeq, Ordering.Lt));

            Assert.False(SequenceUtils.IsOrdered(ltEqSeq, Ordering.Gt));
            Assert.False(SequenceUtils.IsOrdered(ltEqSeq, Ordering.GtOrEq));
            Assert.False(SequenceUtils.IsOrdered(gtEqSeq, Ordering.Lt));
            Assert.False(SequenceUtils.IsOrdered(gtEqSeq, Ordering.LtOrEq));

            Assert.False(SequenceUtils.IsOrdered(seq, Ordering.Gt));
            Assert.False(SequenceUtils.IsOrdered(seq, Ordering.Lt));

            Assert.True(SequenceUtils.IsOrdered(new int[] {1}, Ordering.Lt));
            Assert.True(SequenceUtils.IsOrdered(new int[] {1}, Ordering.LtOrEq));
            Assert.True(SequenceUtils.IsOrdered(new int[] {1}, Ordering.Gt));
            Assert.True(SequenceUtils.IsOrdered(new int[] {1}, Ordering.GtOrEq));
            Assert.True(SequenceUtils.IsOrdered(new int[] {}, Ordering.Lt));
            Assert.True(SequenceUtils.IsOrdered(new int[] {}, Ordering.LtOrEq));
            Assert.True(SequenceUtils.IsOrdered(new int[] {}, Ordering.Gt));
            Assert.True(SequenceUtils.IsOrdered(new int[] {}, Ordering.GtOrEq));
        }

        [Test]
        public void TestShuffle() {
            var results = new int[10];
            var amount = 10000;
            for (var i = 0; i < amount; i++) {
                var vector = Enumerable.Range(0, results.Length).ToArray();
                SequenceUtils.Shuffle(vector);
                for (var j = 0; j < vector.Length; j++) {
                    if (vector[j] == j) {
                        ++results[j];
                    }
                }
            }

            var mean = results.Sum() / results.Length;
            var deviation = 0.0;
            foreach (var i in results) {
                deviation = deviation + (i - mean) * (i - mean);
            }
            deviation = Math.Sqrt(deviation / results.Length);
            Assert.Less(deviation / amount * 100, 1);
        }
    }
}
