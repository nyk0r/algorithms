using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    internal class BinarySearchTreeTest {
        private static string[] _fixture = { "Z", "E", "A", "R", "C", "H", "E", "X", "A", "N", "P", "L", "E"  };
        private static Tuple<string, int>[] _result = {
            Tuple.Create("A", 8),
            Tuple.Create("C", 4),
            Tuple.Create("E", 12),
            Tuple.Create("H", 5),
            Tuple.Create("L", 11),
            Tuple.Create("N", 9),
            Tuple.Create("P", 10),
            Tuple.Create("R", 3),
            Tuple.Create("Z", 0),
            Tuple.Create("X", 7)
        }; 

        private static BinarySearchTree<string, int> CreateBST() {;
        var result = new BinarySearchTree<string, int>(StringComparer.Ordinal);
            for (int i = 0; i < _fixture.Length; i++) {
                result.Put(_fixture[i], i);
            }
            return result;
        }

        [Test]
        public void TestPut() {
            var bst = CreateBST();
            Assert.DoesNotThrow(() => { bst.Put("A", 100); });
            Assert.DoesNotThrow(() => { bst.Put("XXX", 0); });
            Assert.Throws<ArgumentNullException>(() => { bst.Put(null, -1); });
        }

        [Test]
        public void TestGet() {
            var bst = CreateBST();
            int val;
            bool success;
            foreach (var result in _result) {
                success = bst.TryGet(result.Item1, out val);
                Assert.True(success);
                Assert.AreEqual(result.Item2, val);
            }
            success = bst.TryGet("XXX", out val);
            Assert.False(success);
            Assert.Throws<ArgumentNullException>(() => { bst.TryGet(null, out val); });
        }

        [Test]
        public void TestIsEmpty() {
            var bst = new BinarySearchTree<char, int>();
            Assert.True(bst.IsEmpty());
            bst.Put('A', 1);
            Assert.False(bst.IsEmpty());
        }

        [Test]
        public void TestContains() {
            var bst = CreateBST();
            Assert.True(bst.Contains("A"));
            Assert.False(bst.Contains("XXX"));
            Assert.Throws<ArgumentNullException>(() => { new BinarySearchTree<string, string>().Contains(null); });
        }

        [Test]
        public void TestSize() {
            var bst = new BinarySearchTree<char, int>();
            Assert.AreEqual(0, bst.Size());
            bst.Put('A', 1);
            Assert.AreEqual(1, bst.Size());

            Assert.AreEqual(_result.Length, CreateBST().Size());
        }

        [Test]
        public void DeleteMin() {
            var bst = CreateBST();
            var orderedKeys = _result.Select(r => r.Item1).OrderBy(k => k);
            foreach (var key in orderedKeys) {
                Assert.True(bst.Contains(key));
                bst.DeleteMin();
                Assert.False(bst.Contains(key));
            }
            Assert.True(bst.IsEmpty());
        }

        [Test]
        public void TestDeleteMax() {
            var bst = CreateBST();
            var orderedKeys = _result.Select(r => r.Item1).OrderByDescending(k => k);
            foreach (var key in orderedKeys) {
                Assert.True(bst.Contains(key));
                bst.DeleteMax();
                Assert.False(bst.Contains(key));
            }
            Assert.True(bst.IsEmpty());
        }

        [Test]
        public void TestDelete() {
            var bst = CreateBST();
            var keys = _result.Select(r => r.Item1).ToList();
            SequenceUtils.Shuffle(keys);
            foreach (var key in keys) {
                Assert.True(bst.Contains(key));
                bst.Delete(key);
                Assert.False(bst.Contains(key));
            }
            Assert.True(bst.IsEmpty());
        }

        [Test]
        public void TestFloor() {
            var bst = CreateBST();
            var orderedKeys = _result.Select(r => r.Item1).OrderBy(k => k).ToList();
            for (var idx = 0; idx < orderedKeys.Count; idx++) {
                KeyValuePair<string, int> floor;
                var success = bst.TryFloor(orderedKeys[idx], out floor);
                Assert.True(success);
                Assert.AreEqual(orderedKeys[idx], floor.Key);

                success = bst.TryFloor(((char)(orderedKeys[idx][0] + 1)).ToString(), out floor);
                Assert.True(success);
                Assert.AreEqual(orderedKeys[idx], floor.Key);

                success = bst.TryFloor(((char)(orderedKeys[idx][0] - 1)).ToString(), out floor);
                if (idx > 0) {
                    Assert.True(success);
                    Assert.AreEqual(orderedKeys[idx - 1], floor.Key);
                } else {
                    Assert.False(success);
                }
            }
        }

        [Test]
        public void TestCeil() {
            var bst = CreateBST();
            var orderedKeys = _result.Select(r => r.Item1).OrderBy(k => k).ToList();
            for (var idx = 0; idx < orderedKeys.Count; idx++) {
                KeyValuePair<string, int> ceil;
                var success = bst.TryCeil(orderedKeys[idx], out ceil);
                Assert.True(success);
                Assert.AreEqual(orderedKeys[idx], ceil.Key);

                success = bst.TryCeil(((char)(orderedKeys[idx][0] - 1)).ToString(), out ceil);
                Assert.True(success);
                Assert.AreEqual(orderedKeys[idx], ceil.Key);

                success = bst.TryCeil(((char)(orderedKeys[idx][0] + 1)).ToString(), out ceil);
                if (idx < orderedKeys.Count - 1) {
                    Assert.True(success);
                    Assert.AreEqual(orderedKeys[idx + 1], ceil.Key);
                } else {
                    Assert.False(success);
                }
            }
        }

        [Test]
        public void TestRank() {
            var bst = CreateBST();
            var orderedKeys = _result.Select(r => r.Item1).OrderBy(k => k).ToList();
            for (var idx = 0; idx < orderedKeys.Count; idx++) {
                Assert.AreEqual(idx, bst.Rank(orderedKeys[idx]));
            }
        }
    }
}
