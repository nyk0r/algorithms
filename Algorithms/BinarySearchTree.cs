using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Algorithms {
    public class BinarySearchTree<TKey, TValue> {
        private class Node {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            public Node Left { get; set; }
            public Node Right { get; set; }

            public int Size { get; set; }
            public int Height { get; set; }

            public Node(TKey key, TValue value) {
                Key = key;
                Value = value;
                Size = 1;
                Height = 0;
            }

            public void UpdateAugumentations() {
                // Amount of nodes in the tree rooted at the node.
                Size = 1;
                Size += Left != null ? Left.Size : 0;
                Size += Right != null ? Right.Size : 0;

                // Height (maximum edges count needed to go down from the node to a leaf) of the tree rooted at the node.
                Height = Right != null || Left != null ? 1 : 0;
                Height += Math.Max(
                    Left != null ? Left.Height : 0,
                    Right != null ? Right.Height : 0
                );
            }

            public KeyValuePair<TKey, TValue> ToKeyValuePair() {
                return new KeyValuePair<TKey, TValue>(Key, Value);
            } 
        }

        private readonly IComparer<TKey> _comparer;
        private Node _root;

        public BinarySearchTree(IComparer<TKey> comparer) {
            _comparer = comparer;
        }

        public BinarySearchTree() : this(Comparer<TKey>.Default) { }

        #region Insert and retrieve
        private Node Put(Node root, TKey key, TValue value) {
            if (root == null) {
                return new Node(key, value);
            }
            var cmp = _comparer.Compare(key, root.Key);
            if (cmp > 0) {
                root.Right = Put(root.Right, key, value);
            } else if (cmp < 0) {
                root.Left = Put(root.Left, key, value);
            } else {
                root.Value = value;
            }
            root.UpdateAugumentations();
            return root;
        }

        public void Put(TKey key, TValue value) {
            if (key == null) {
                throw new ArgumentNullException();
            }
            _root = Put(_root, key, value);
        }

        private Node Get(Node root, TKey key) {
            if (key == null) {
                throw new ArgumentNullException();
            }

            while (root != null) {
                var cmp = _comparer.Compare(key, root.Key);
                if (cmp > 0) {
                    root = root.Right;
                } else if (cmp < 0) {
                    root = root.Left;
                } else {
                    return root;
                }
            }
            return null;
        }

        public bool TryGet(TKey key, out TValue value) {
            var node = Get(_root, key);
            if (node != null) {
                value = node.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        public bool Contains(TKey key) {
            return Get(_root, key) != null;
        }

        public bool IsEmpty() {
            return _root == null;
        }

        /// <returns>Size of the BST.</returns>
        public int Size() {
            return _root != null ? _root.Size : 0;
        }

        /// <returns>Size of the subtree rooted at the key node.</returns>
        public int Size(TKey key) {
            var node = Get(_root, key);
            if (node == null) {
                throw new KeyNotFoundException();
            }
            return node.Size;
        }
        #endregion

        #region Delete node
        private Node Delete(Node root, TKey key) {
            if (root == null) {
                return null;
            }
            var cmp = _comparer.Compare(key, root.Key);
            if (cmp > 0) {
                root.Right = Delete(root.Right, key);
            } else if (cmp < 0) {
                root.Left = Delete(root.Left, key);
            } else {
                if (root.Right == null) {
                    return root.Left;
                }
                if (root.Left == null) {
                    return root.Right;
                }

                var minRight = Min(root.Right);
                root.Right = DeleteMin(root.Right);
                root.Key = minRight.Key;
                root.Value = minRight.Value;
            }
            root.UpdateAugumentations();
            return root;
        }

        public void Delete(TKey key) {
            _root = Delete(_root, key);
        }

        private Node DeleteMin(Node root) {
            if (root.Left == null) {
                return root.Right;
            }
            root.Left = DeleteMin(root.Left);
            root.UpdateAugumentations();
            return root;
        }

        public void DeleteMin() {
            _root = DeleteMin(_root);
        }

        private Node DeleteMax(Node root) {
            if (root.Right == null) {
                return root.Left;
            }
            root.Right = DeleteMax(root.Right);
            root.UpdateAugumentations();
            return root;
        }

        public void DeleteMax() {
            _root = DeleteMax(_root);
        }
        #endregion

        #region Get min/max node
        private Node Min(Node root) {
            for (; root != null; root = root.Left) {
                if (root.Left == null) {
                    return root;
                }
            }
            return null;
        }

        public bool TryGetMin(out KeyValuePair<TKey, TValue> min) {
            var minNode = Min(_root);
            if (minNode != null) {
                min = minNode.ToKeyValuePair();
                return true;
            }
            min = new KeyValuePair<TKey, TValue>();
            return false;
        }

        private Node Max(Node root) {
            for (; root != null; root = root.Right) {
                if (root.Right == null) {
                    return root;
                }
            }
            return null;
        }

        public bool TryGetMax(out KeyValuePair<TKey, TValue> max)
        {
            var maxNode = Max(_root);
            if (maxNode != null) {
                max = maxNode.ToKeyValuePair();
                return true;
            }
            max = new KeyValuePair<TKey, TValue>();
            return false;
        }
        #endregion

        #region Floor and Ceiling
        private Node Floor(Node root, TKey key) {
            if (root == null) {
                return null;
            }
            var cmp = _comparer.Compare(key, root.Key);
            if (cmp == 0) {
                return root;
            } else if (cmp < 0) {
                return Floor(root.Left, key);
            } else {
                var better = Floor(root.Right, key);
                return better ?? root;
            }
        }

        public bool TryFloor(TKey key, out KeyValuePair<TKey, TValue> floor) {
            var node = Floor(_root, key);
            if (node != null) {
                floor = node.ToKeyValuePair();
                return true;
            }
            floor = new KeyValuePair<TKey, TValue>();
            return false;
        }

        private Node Ceil(Node root, TKey key) {
            if (root == null) {
                return null;
            }
            var cmp = _comparer.Compare(key, root.Key);
            if (cmp == 0) {
                return root;
            } else if (cmp > 0) {
                return Ceil(root.Right, key);
            } else {
                var better = Ceil(root.Left, key);
                return better ?? root;
            }
        }

        public bool TryCeil(TKey key, out KeyValuePair<TKey, TValue> ceil) {
            var node = Ceil(_root, key);
            if (node != null) {
                ceil = node.ToKeyValuePair();
                return true;
            }
            ceil = new KeyValuePair<TKey, TValue>();
            return false;
        }
        #endregion

        #region Size of a slice
        private int Rank(Node root, TKey key) {
            if (root == null) {
                return 0;
            }
            var cmp = _comparer.Compare(key, root.Key);
            if (cmp < 0) {
                return Rank(root.Left, key);
            } else if (cmp > 0) {
                return root.Size;
            } else {
                return root.Left != null ? root.Left.Size : 0;
            }
        }

        public int Rank(TKey key) {
            return Rank(_root, key);
        }
        #endregion

        public TKey Select(int k) {
            throw new NotImplementedException();
        }

        public int Size(TKey lo, TKey hi) {
            throw new NotImplementedException();
        }

        public IEnumerable<TKey> Keys(TKey lo, TKey hi) {
            throw new NotImplementedException();
        }

        public IEnumerable<TKey> Keys() {
            throw new NotImplementedException();
        }
    }
}