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
    }
}
