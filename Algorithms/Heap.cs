using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Algorithms {
    public enum HeapType { Max, Min }

    public class OrderedHeap<T> : IEnumerable<T> {
        private Func<T, T, bool> _comparer;
        private IList<T> _content; 

        public OrderedHeap(IEnumerable<T> source, IComparer<T> comparer, HeapType type) {
            switch (type) {
                case HeapType.Max:
                    _comparer = (a, b) => comparer.Compare(a, b) >= 0;
                    break;
                case HeapType.Min:
                    _comparer = (a, b) => comparer.Compare(a, b) <= 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
            _content = new List<T>(source);
            for (var node = _content.Count/2; node > 0; --node) {
                Sink(node);
            }
        }

        public OrderedHeap(IEnumerable<T> source, HeapType type) : this(source, Comparer<T>.Default, type) { }

        public OrderedHeap(HeapType type) : this(new T[] { }, type) { }

        public void Add(T item) {
            _content.Add(item);
            Swim(_content.Count);
        }

        public T GetPeak() {
            if (_content.Count == 0) {
                throw new InvalidOperationException("The heap is empty.");
            }

            var result = _content[0];
            _content[0] = _content[_content.Count - 1];
            _content.RemoveAt(_content.Count - 1);
            Sink(1);
            return result;
        }

        public T Peak { get { return _content[0]; } }

        public int Count { get { return _content.Count; }}

        private void Swim(int node) {
            for (var parent = node/2; 
                parent > 0 && !_comparer(Get(parent), Get(node)); 
                node = parent, parent = node/2) {
                Swap(parent, node);
            }
        }

        private void Sink(int node) {
            for (var max = Max(node*2, node*2 + 1);
                max != -1 && Max(node, max) != node;
                node = max, max = Max(node*2, node*2 + 1)) {
                Swap(node, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Max(int node1, int node2) {
            if (node1 > _content.Count) {
                return -1;
            }
            if (node2 > _content.Count) {
                return node1;
            }
            return _comparer(Get(node1), Get(node2)) ? node1 : node2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T Get(int node) {
            return _content[node - 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Swap(int node1, int node2) {
            var tmp = _content[node1 - 1];
            _content[node1 - 1] = _content[node2 - 1];
            _content[node2 - 1] = tmp;
        }

        public IEnumerator<T> GetEnumerator() {
            return _content.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
