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

        private bool TryGetKeyValuePair(Func<Node> action, out KeyValuePair<TKey, TValue> result) {
            var tmp = action();
            if (tmp != null) {
                result = tmp.ToKeyValuePair();
                return true;
            }
            result = new KeyValuePair<TKey, TValue>();
            return false;
        }

        #region Insert and retrieve
        public void Put(TKey key, TValue value) {
            if (key == null) {
                throw new ArgumentNullException();
            }

            if (_root == null) {
                _root = new Node(key, value);
                _root.UpdateAugumentations();
                return;
            }

            var root = _root;
            var nodesStack = new Stack<Node>(Size() + 1);
            while (root != null) {
                nodesStack.Push(root);
                var cmp = _comparer.Compare(key, root.Key);
                if (cmp == 0) {
                    root.Value = value;
                    return; // no need for augumentations updates
                }

                if (cmp > 0) {
                    if (root.Right == null) {
                        root.Right = new Node(key, value);
                        nodesStack.Push(root.Right);
                        break;
                    }
                    root = root.Right;
                } else if (cmp < 0) {
                    if (root.Left == null) {
                        root.Left = new Node(key, value);
                        nodesStack.Push(root.Left);
                        break;
                    }
                    root = root.Left;
                }
            }
            foreach (var node in nodesStack) {
                node.UpdateAugumentations();
            }
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

                // Taking the min key elenent from the right subtree.
                // It is also possible to take the max key element the left subtree.
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
            return TryGetKeyValuePair(() => Min(_root), out min);
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
            return TryGetKeyValuePair(() => Max(_root), out max);
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
            return TryGetKeyValuePair(() => Floor(_root, key), out floor);
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
            return TryGetKeyValuePair(() => Ceil(_root, key), out ceil);
        }
        #endregion

        #region Size of a slice
        private int Rank(Node root, TKey key) {
            if (root == null) {
                return 0;
            }
            var cmp = _comparer.Compare(key, root.Key);
            if (cmp > 0) {
                return 1 + (root.Left != null ? root.Left.Size : 0) + Rank(root.Right, key);
            } else if (cmp < 0) {
                return Rank(root.Left, key);
            } else {
                return root.Left != null ? root.Left.Size : 0;
            }
        }

        public int Rank(TKey key) {
            return Rank(_root, key);
        }
        #endregion

        private Node Select(Node root, int smallerKeysCount) {
            if (root == null) {
                return null;
            }
            var leftSize = root.Left != null ? root.Left.Size : 0;
            if (leftSize > smallerKeysCount) {
                return Select(root.Left, smallerKeysCount);
            } else if (leftSize < smallerKeysCount) {
                return Select(root.Left, smallerKeysCount - leftSize - 1);
            } else {
                return root;
            }
        }

        public bool TrySelect(int smallerKeysCount, out KeyValuePair<TKey, TValue> upper) {
            return TryGetKeyValuePair(() => Select(_root, smallerKeysCount), out upper);
        }

        private void VisitNodesRange(Node root, TKey lo, TKey hi, Action<Node> action) {
            if (root == null) {
                return;
            }
            var cmplo = _comparer.Compare(lo, root.Key);
            var cmphi = _comparer.Compare(hi, root.Key);
            if (cmplo < 0) {
                VisitNodesRange(root.Left, lo, hi, action);
            }
            if (cmplo <= 0 && cmphi >= 0) {
                action(root);
            }
            if (cmphi > 0) {
                VisitNodesRange(root.Right, lo, hi, action);
            }
        }

        public int Size(TKey lo, TKey hi) {
            var result = 0;
            VisitNodesRange(_root, lo, hi, n => { ++result; });
            return result;
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetItems(TKey lo, TKey hi) {
            var items = new List<KeyValuePair<TKey, TValue>>(Size());
            VisitNodesRange(_root, lo, hi, n => { items.Add(n.ToKeyValuePair()); });
            return items;
        }

        private void VisitNodeInOrder(Node root, Action<Node> action) {
            if (root == null) {
                return;
            }

            VisitNodeInOrder(root.Left, action);
            action(root);
            VisitNodeInOrder(root.Right, action);
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetItems() {
            var items = new List<KeyValuePair<TKey, TValue>>(Size());
            VisitNodeInOrder(_root, n => { items.Add(n.ToKeyValuePair()); });
            return items;
        }
    }
}