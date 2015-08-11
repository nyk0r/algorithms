using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Algorithms;

namespace AlgorithmsTests {
    [TestFixture]
    internal class BinarySearchTreeTest {
        private static string[] _fixture = { "S", "E", "A", "R", "C", "H", "E", "X", "A", "M", "P", "L", "E" };
        private static Tuple<string, int>[] _result = {
            Tuple.Create("A", 8),
            Tuple.Create("C", 4),
            Tuple.Create("E", 12),
            Tuple.Create("H", 5),
            Tuple.Create("L", 11),
            Tuple.Create("M", 9),
            Tuple.Create("P", 10),
            Tuple.Create("R", 3),
            Tuple.Create("S", 0),
            Tuple.Create("X", 7)
        }; 

        private static BinarySearchTree<string, int> CreateBST() {;
            var result = new BinarySearchTree<string, int>();
            for (int i = 0; i < _fixture.Length; i++) {
                result.Put(_fixture[i], i);
            }
            return result;
        }

        [Test]
        public void TestPut() {
            var bst = CreateBST();
            Assert.DoesNotThrow(() => { bst.Put("A", 100); });
            Assert.DoesNotThrow(() => { bst.Put("Z", 0); });
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
            success = bst.TryGet("Z", out val);
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
            Assert.False(bst.Contains("Z"));
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

        /*[Test]
        public void TestFloor() {
            var bst = CreateBST();
            var orderedKeys = _result.Select(r => r.Item1).OrderByDescending(k => k).ToList();
            KeyValuePair<string, int> ceil;
            bool success;
            for (var idx = 0; idx < orderedKeys.Count - 1; idx++) {
                success = bst.TryFloor(orderedKeys[idx], out ceil);
                Assert.True(success);
                Assert.AreEqual(orderedKeys[idx + 1], ceil.Key);
            }
            Assert.False(bst.TryFloor(orderedKeys[orderedKeys.Count - 1], out ceil));
        }

        [Test]
        public void TestCeil() {
            var bst = CreateBST();
            var orderedKeys = _result.Select(r => r.Item1).OrderBy(k => k).ToList();
            KeyValuePair<string, int> ceil;
            bool success;
            for (var idx = 0; idx < orderedKeys.Count - 1; idx++) {
                success = bst.TryCeil(orderedKeys[idx], out ceil);
                Assert.True(success);
                Assert.AreEqual(orderedKeys[idx + 1], ceil.Key);
            }
            Assert.False(bst.TryCeil(orderedKeys[orderedKeys.Count - 1], out ceil));
        }

        [Test]
        public void TestRank() {
            var bst = CreateBST();
            var orderedKeys = _result.Select(r => r.Item1).OrderBy(k => k).ToList();
            for (var idx = 0; idx < orderedKeys.Count; idx++) {
                Assert.AreEqual(idx, bst.Rank(orderedKeys[idx]));
            }
        }*/
    }
}
